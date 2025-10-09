--
-- PostgreSQL database dump
--

\restrict xkTRIMwGJ28BttYE88qhYi7jTl0EHP7ONLWaRt3LyUIJWFzu0agl5JEpDkq448Z

-- Dumped from database version 18.0
-- Dumped by pg_dump version 18.0

-- Started on 2025-10-09 00:58:37

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 922 (class 1247 OID 16657)
-- Name: catalog_of_works_category; Type: TYPE; Schema: public; Owner: -
--

CREATE TYPE public.catalog_of_works_category AS ENUM (
    'Диагностика и обслуживание',
    'Двигатель и система выпуска',
    'Ходовая часть и рулевое',
    'Тормозная система',
    'Трансмиссия',
    'Шины и колеса',
    'Электрика и электроника',
    'Кузовные работы'
);


--
-- TOC entry 904 (class 1247 OID 16411)
-- Name: expenses_category_enum; Type: TYPE; Schema: public; Owner: -
--

CREATE TYPE public.expenses_category_enum AS ENUM (
    'Зарплата',
    'Запчасти',
    'Аренда',
    'Коммунальные',
    'Реклама',
    'Оборудование',
    'Хозяйственные',
    'Налоги',
    'Ремонт',
    'Связь',
    'Страхование'
);


--
-- TOC entry 907 (class 1247 OID 16434)
-- Name: expenses_type_enum; Type: TYPE; Schema: public; Owner: -
--

CREATE TYPE public.expenses_type_enum AS ENUM (
    'Переменный',
    'Постоянный',
    'Капитальный'
);


--
-- TOC entry 961 (class 1247 OID 16881)
-- Name: payment_method; Type: TYPE; Schema: public; Owner: -
--

CREATE TYPE public.payment_method AS ENUM (
    'Картой',
    'Наличными',
    'ЕРИП',
    'Рассрочка',
    'Другое'
);


--
-- TOC entry 973 (class 1247 OID 19833)
-- Name: priority; Type: TYPE; Schema: public; Owner: -
--

CREATE TYPE public.priority AS ENUM (
    'Повышенный',
    'Общий',
    'Низкий'
);


--
-- TOC entry 928 (class 1247 OID 16692)
-- Name: role_name; Type: TYPE; Schema: public; Owner: -
--

CREATE TYPE public.role_name AS ENUM (
    'Менеджер',
    'Клиент',
    'Работник'
);


--
-- TOC entry 898 (class 1247 OID 16390)
-- Name: tax_type_enum; Type: TYPE; Schema: public; Owner: -
--

CREATE TYPE public.tax_type_enum AS ENUM (
    'республиканский',
    'внебюджетный',
    'местный',
    'целевой'
);


--
-- TOC entry 273 (class 1255 OID 18418)
-- Name: fn_additional_work_decision(); Type: FUNCTION; Schema: public; Owner: -
--

CREATE FUNCTION public.fn_additional_work_decision() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE 
    order_exists BOOLEAN;
    work_exists BOOLEAN;
    order_closed BOOLEAN;
    standard_time DECIMAL(6,2);
    existing_bill_id INT;
    proposed_part RECORD;
BEGIN
    RAISE NOTICE '=== ТРИГГЕР АКТИВИРОВАН ===';
    RAISE NOTICE 'Старое решение: %, Новое решение: %', OLD.client_decision, NEW.client_decision;
    
    -- Используем ID статусов вместо текста
    IF NEW.client_decision = 6 AND (OLD.client_decision IS NULL OR OLD.client_decision != 6) THEN
        RAISE NOTICE 'Условие принятия выполнено! Проверяем данные...';

        -- Проверка существования заказа
        SELECT EXISTS (SELECT 1 FROM work_orders WHERE work_order_id = NEW.order_id)
        INTO order_exists;
        
        IF NOT order_exists THEN
            RAISE EXCEPTION 'Заказ с ID % не существует', NEW.order_id;
        END IF;
        RAISE NOTICE 'Заказ существует: %', order_exists;

        -- Проверка, что заказ не закрыт или оплачен (статусы 1 и 5)
        SELECT EXISTS (
            SELECT 1 
            FROM work_orders wo
            WHERE wo.work_order_id = NEW.order_id 
            AND wo.work_order_status_id IN (1, 5)
        ) INTO order_closed;

        IF order_closed THEN
            RAISE EXCEPTION 'Заказ с ID % закрыт или оплачен', NEW.order_id;
        END IF;
        RAISE NOTICE 'Заказ не закрыт: %', NOT order_closed;

        -- Проверка существования работы в каталоге
        SELECT EXISTS (
            SELECT 1 
            FROM catalog_of_works 
            WHERE job_id = NEW.proposed_work_id
        ) INTO work_exists;

        IF NOT work_exists THEN
            RAISE EXCEPTION 'Работа с ID % не существует в каталоге', NEW.proposed_work_id;
        END IF;
        RAISE NOTICE 'Работа существует: %', work_exists;

        -- Получаем нормативное время выполнения работы
        SELECT catalog_of_works_standard_time 
        INTO standard_time
        FROM catalog_of_works
        WHERE job_id = NEW.proposed_work_id;

        RAISE NOTICE 'Стандартное время работы: % часов', standard_time;

        -- Создаем запись в work_in_order
        RAISE NOTICE 'Добавляем запись в work_in_order...';
        INSERT INTO work_in_order (
            work_order_id,
            work_in_order_job_id, 
            work_in_order_worker_id,
            work_in_order_time_spent
        ) VALUES (
            NEW.order_id,
            NEW.proposed_work_id,
            NEW.proposed_by_worker_id,
            COALESCE(standard_time, 0)
        );

        RAISE NOTICE 'Запись успешно добавлена в work_in_order!';

        -- Добавляем запчасти из proposed_parts
        FOR proposed_part IN 
            SELECT 
                pp.proposed_parts_used_part_id,
                up.used_part_name,
                up.used_part_article
            FROM proposed_parts pp
            JOIN used_parts up ON pp.proposed_parts_used_part_id = up.used_part_id
            WHERE pp.proposed_parts_proposal_id = NEW.proposal_id
        LOOP
            RAISE NOTICE 'Добавляем запчасть: % (артикул: %, ID: %)', 
                proposed_part.used_part_name, 
                proposed_part.used_part_article,
                proposed_part.proposed_parts_used_part_id;
            
            -- Обновляем запись запчасти, привязывая ее к заказу
            UPDATE used_parts 
            SET used_parts_order_id = NEW.order_id
            WHERE used_part_id = proposed_part.proposed_parts_used_part_id;
        END LOOP;
        
        RAISE NOTICE 'Все связанные запчасти добавлены в заказ!';

        -- Проверяем существование счета
        SELECT bill_id INTO existing_bill_id
        FROM bills 
        WHERE bill_order_id = NEW.order_id
        LIMIT 1;

        -- Используем функцию пересчета
        IF existing_bill_id IS NOT NULL THEN
            RAISE NOTICE 'Счет существует, пересчитываем сумму...';
            PERFORM recalculate_bill_total(existing_bill_id);
            RAISE NOTICE 'Счет пересчитан!';
        ELSE
            RAISE NOTICE 'Счет не существует, создание не требуется';
        END IF;

        -- Устанавливаем статус предложения в 6 (Принят)
        NEW.proposal_status := 6;
        
    -- Обработка отклонения (статус 7)
    ELSIF NEW.client_decision = 7 AND (OLD.client_decision IS NULL OR OLD.client_decision != 7) THEN
        RAISE NOTICE 'Предложение отклонено клиентом';
        NEW.proposal_status := 7;
        
    ELSE
        RAISE NOTICE 'Условие не выполнено - вставки не будет';
    END IF;
    
    RAISE NOTICE '=== ТРИГГЕР ЗАВЕРШЕН ===';
    RETURN NEW;
END;
$$;


--
-- TOC entry 257 (class 1255 OID 16945)
-- Name: fn_bill_total_sum(); Type: FUNCTION; Schema: public; Owner: -
--

CREATE FUNCTION public.fn_bill_total_sum() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
	DECLARE
		bill_cost NUMERIC := 0;
		work_cost NUMERIC := 0;
		part_cost NUMERIC := 0;
	BEGIN
	
	SELECT SUM(catalog_of_works_standard_time * worker_hourly_rate) 
	INTO work_cost
	FROM work_in_order wio
	LEFT JOIN catalog_of_works cof ON wio.work_in_order_job_id = cof.job_id
	LEFT JOIN workers w ON w.worker_id = wio.work_in_order_worker_id 
	WHERE wio.work_order_id = NEW.bill_order_id; 
	
	SELECT SUM(used_part_total_sum)
	INTO part_cost
	FROM used_parts
	WHERE used_parts_order_id = NEW.bill_order_id;
			
	bill_cost := COALESCE (work_cost, 0) + COALESCE (part_cost, 0);

	NEW.bill_total_sum := bill_cost;
	RETURN NEW;
END;

$$;


--
-- TOC entry 276 (class 1255 OID 17084)
-- Name: fn_payment_journal_pay_check(); Type: FUNCTION; Schema: public; Owner: -
--

CREATE FUNCTION public.fn_payment_journal_pay_check() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    total_paid NUMERIC := 0;
    bill_total NUMERIC := 0;
BEGIN
    -- заблокировать счёт, чтобы избежать гонок
    PERFORM 1 FROM bills WHERE bill_id = NEW.payment_bill_id FOR UPDATE;

    SELECT COALESCE(SUM(payment_amount),0)
    INTO total_paid
    FROM payment_journal
    WHERE payment_bill_id = NEW.payment_bill_id;

    SELECT bill_total_sum INTO bill_total
    FROM bills
    WHERE bill_id = NEW.payment_bill_id;

    IF total_paid >= bill_total THEN
        UPDATE bills SET bill_status_id = 1 WHERE bill_id = NEW.payment_bill_id;
        UPDATE work_orders
        SET work_order_status_id = 1
        WHERE work_order_id = (SELECT bill_order_id FROM bills WHERE bill_id = NEW.payment_bill_id);
    ELSIF total_paid > 0 THEN
        UPDATE bills SET bill_status_id = 3 WHERE bill_id = NEW.payment_bill_id;
    ELSE
        UPDATE bills SET bill_status_id = 2 WHERE bill_id = NEW.payment_bill_id;
    END IF;

    RETURN NEW;
END;
$$;


--
-- TOC entry 270 (class 1255 OID 19830)
-- Name: fn_set_actual_closing_date(); Type: FUNCTION; Schema: public; Owner: -
--

CREATE FUNCTION public.fn_set_actual_closing_date() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
 	RAISE NOTICE '=== ТРИГГЕР ДОБАВЛЕНИЯ ДАТЫ ПОГАШЕНИЯ СЧЕТА НАЧАТ ===';
    IF NEW.bill_status_id = 1 AND OLD.bill_status_id != 1 THEN
		NEW.actual_closing_bill_date := CURRENT_DATE;  
	END IF;
	RAISE NOTICE '=== ТРИГГЕР ДОБАВЛЕНИЯ ДАТЫ ПОГАШЕНИЯ СЧЕТА ЗАВЕРШЕН ===';
    RETURN NEW;
END;
$$;


--
-- TOC entry 263 (class 1255 OID 18856)
-- Name: fn_work_in_order_insert_job_check(); Type: FUNCTION; Schema: public; Owner: -
--

CREATE FUNCTION public.fn_work_in_order_insert_job_check() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE 
    duplicate_exists BOOLEAN;
BEGIN
    RAISE NOTICE '=== ТРИГГЕР ПРОВЕРКИ ДУБЛИКАТА РАБОТ НАЧАТ ===';
    
    -- Проверяем, есть ли уже такая работа в этом заказе
    SELECT EXISTS (
        SELECT 1 
        FROM work_in_order 
        WHERE work_order_id = NEW.work_order_id 
        AND work_in_order_job_id = NEW.work_in_order_job_id
    ) INTO duplicate_exists;
    
    IF duplicate_exists THEN
        RAISE EXCEPTION 'Работа ID % уже существует в заказе ID %. Дублирование запрещено.', 
            NEW.work_in_order_job_id, NEW.work_order_id;
    ELSE
        RAISE NOTICE 'Дубликатов не найдено - вставка разрешена';
    END IF;
    
    RAISE NOTICE '=== ТРИГГЕР ЗАВЕРШЕН ===';
    RETURN NEW;
END;
$$;


--
-- TOC entry 275 (class 1255 OID 19051)
-- Name: fn_work_in_order_job_check(); Type: FUNCTION; Schema: public; Owner: -
--

CREATE FUNCTION public.fn_work_in_order_job_check() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE 
    duplicate_exists BOOLEAN;
    order_closed BOOLEAN;
BEGIN
    -- Проверяем, закрыт ли заказ
    SELECT EXISTS (
        SELECT 1 
        FROM work_orders 
        WHERE work_order_id = NEW.work_order_id 
        AND work_order_status_id IN (1, 5) -- закрытые статусы
    ) INTO order_closed;
    
    IF order_closed THEN
        RAISE EXCEPTION 'Заказ ID % закрыт, добавление работ запрещено', NEW.work_order_id;
    END IF;

    -- Проверяем дубликаты
    SELECT EXISTS (
        SELECT 1 
        FROM work_in_order 
        WHERE work_order_id = NEW.work_order_id 
        AND work_in_order_job_id = NEW.work_in_order_job_id
        AND work_note_id != COALESCE(NEW.work_note_id, -1) -- исключаем текущую запись при UPDATE
    ) INTO duplicate_exists;
    
    IF duplicate_exists THEN
        RAISE EXCEPTION 'Работа ID % уже существует в заказе ID %. Дублирование запрещено.', 
            NEW.work_in_order_job_id, NEW.work_order_id;
    END IF;
    
    RETURN NEW;
END;
$$;


--
-- TOC entry 274 (class 1255 OID 18598)
-- Name: fn_work_order_add_in_history(); Type: FUNCTION; Schema: public; Owner: -
--

CREATE FUNCTION public.fn_work_order_add_in_history() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE 
    service_sum DECIMAL(12,2) := 0;
    bill_id_val INT;
BEGIN
    RAISE NOTICE '=== ТРИГГЕР ИСТОРИИ ОБСЛУЖИВАНИЯ АКТИВИРОВАН ===';
    
    IF NEW.work_order_status_id = 5 AND OLD.work_order_status_id != 5 THEN
        RAISE NOTICE 'Заказ % закрывается, добавляем в историю...', NEW.work_order_id;
        
        -- Находим ID счета для этого заказа
        SELECT bill_id INTO bill_id_val
        FROM bills 
        WHERE bill_order_id = NEW.work_order_id;
        
        IF bill_id_val IS NOT NULL THEN
            -- ПЕРЕСЧИТЫВАЕМ счет перед добавлением в историю
            RAISE NOTICE 'Пересчитываем счет % для актуальной суммы', bill_id_val;
            PERFORM recalculate_bill_total(bill_id_val);
            
            -- Получаем актуальную сумму
            SELECT bill_total_sum INTO service_sum
            FROM bills
            WHERE bill_id = bill_id_val;
        ELSE
            RAISE NOTICE 'Счет для заказа % не найден, используем сумму 0', NEW.work_order_id;
        END IF;
        
        -- Добавляем запись в историю с АКТУАЛЬНОЙ суммой
        INSERT INTO service_history 
        (
            service_history_work_order_id,
            service_history_car_id,
            service_history_work_completion_date,
            service_history_sum
        )
        VALUES
        (
            NEW.work_order_id,
            NEW.work_order_car_id,
            CURRENT_DATE,
            service_sum
        );
        
        RAISE NOTICE 'Запись добавлена в историю с суммой: %', service_sum;
    ELSE
        RAISE NOTICE 'Условие не выполнено - вставки не будет';
    END IF;
    
    RAISE NOTICE '=== ТРИГГЕР ИСТОРИИ ЗАВЕРШЕН ===';
    RETURN NEW;
END;
$$;


--
-- TOC entry 272 (class 1255 OID 19848)
-- Name: fn_work_order_status_inspection(); Type: FUNCTION; Schema: public; Owner: -
--

CREATE FUNCTION public.fn_work_order_status_inspection() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    all_works_completed BOOLEAN;
BEGIN
    IF NEW.work_in_order_status_id != OLD.work_in_order_status_id THEN

	    SELECT NOT EXISTS (
	        SELECT 1 
	        FROM work_in_order 
	        WHERE work_order_id = NEW.work_order_id 
	        AND work_in_order_status_id != 9  
	    ) INTO all_works_completed;
	
	    IF all_works_completed THEN
	        UPDATE work_orders 
	        SET work_order_status_id = 5 
	        WHERE work_order_id = NEW.work_order_id;
	    END IF;
		
	END IF;
    
    RETURN NEW;
END;
$$;


--
-- TOC entry 258 (class 1255 OID 19851)
-- Name: recalculate_bill_total(integer); Type: FUNCTION; Schema: public; Owner: -
--

CREATE FUNCTION public.recalculate_bill_total(p_bill_id integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
DECLARE
    v_total numeric := 0;
BEGIN
    -- блокируем счёт на время пересчёта
    PERFORM 1 FROM bills WHERE bill_id = p_bill_id FOR UPDATE;

    SELECT COALESCE(SUM(wio.work_in_order_time_spent * w.worker_hourly_rate), 0)
         + COALESCE(SUM(up.used_part_total_sum), 0)
    INTO v_total
    FROM bills b
    JOIN work_orders wo ON wo.work_order_id = b.bill_order_id
    LEFT JOIN work_in_order wio ON wio.work_order_id = wo.work_order_id
    LEFT JOIN workers w ON w.worker_id = wio.work_in_order_worker_id
    LEFT JOIN used_parts up ON up.used_parts_order_id = wo.work_order_id
    WHERE b.bill_id = p_bill_id;

    UPDATE bills
    SET bill_total_sum = v_total
    WHERE bill_id = p_bill_id;
END;
$$;


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 254 (class 1259 OID 16910)
-- Name: additional_work_proposals; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.additional_work_proposals (
    proposal_id integer NOT NULL,
    order_id integer NOT NULL,
    proposed_work_id integer NOT NULL,
    proposed_by_worker_id integer NOT NULL,
    proposal_status integer,
    client_decision integer,
    proposed_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT chk_client_decision CHECK ((client_decision = ANY (ARRAY[6, 7]))),
    CONSTRAINT chk_proposal_status CHECK ((proposal_status = ANY (ARRAY[6, 7, 8])))
);


--
-- TOC entry 5230 (class 0 OID 0)
-- Dependencies: 254
-- Name: COLUMN additional_work_proposals.proposal_status; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public.additional_work_proposals.proposal_status IS '6=Принят, 7=Отклонен, 8=В ожидании';


--
-- TOC entry 5231 (class 0 OID 0)
-- Dependencies: 254
-- Name: COLUMN additional_work_proposals.client_decision; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON COLUMN public.additional_work_proposals.client_decision IS '6=Принят, 7=Отклонен';


--
-- TOC entry 253 (class 1259 OID 16909)
-- Name: additional_work_proposals_proposal_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.additional_work_proposals ALTER COLUMN proposal_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.additional_work_proposals_proposal_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 250 (class 1259 OID 16857)
-- Name: bills; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.bills (
    bill_id integer NOT NULL,
    bill_order_id integer NOT NULL,
    bill_status_id integer NOT NULL,
    bill_date date DEFAULT CURRENT_DATE NOT NULL,
    bill_total_sum numeric(12,2) NOT NULL,
    actual_closing_bill_date date,
    last_closing_bill_date date GENERATED ALWAYS AS ((bill_date + '14 days'::interval)) STORED,
    CONSTRAINT chk_bill_total_sum CHECK ((bill_total_sum > (0)::numeric))
);


--
-- TOC entry 249 (class 1259 OID 16856)
-- Name: bills_bill_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.bills ALTER COLUMN bill_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.bills_bill_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 242 (class 1259 OID 16781)
-- Name: cars; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.cars (
    car_id integer NOT NULL,
    car_owner_id integer NOT NULL,
    car_brand character varying(128) NOT NULL,
    car_model character varying(256) NOT NULL,
    car_year_of_manufacture integer NOT NULL,
    car_vin_number character varying(17) NOT NULL,
    car_state_number character varying(16) NOT NULL,
    car_milage integer NOT NULL,
    CONSTRAINT chk_car_milage CHECK ((car_milage >= 0)),
    CONSTRAINT chk_car_vin_number CHECK ((char_length((car_vin_number)::text) = 17)),
    CONSTRAINT chk_car_year_of_manufacture CHECK (((car_year_of_manufacture >= 1900) AND ((car_year_of_manufacture)::numeric <= EXTRACT(year FROM CURRENT_DATE))))
);


--
-- TOC entry 241 (class 1259 OID 16780)
-- Name: cars_car_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.cars ALTER COLUMN car_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.cars_car_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 230 (class 1259 OID 16674)
-- Name: catalog_of_works; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.catalog_of_works (
    job_id integer NOT NULL,
    title_of_work character varying(256) NOT NULL,
    catalog_of_works_category public.catalog_of_works_category NOT NULL,
    catalog_of_works_description text NOT NULL,
    catalog_of_works_standard_time numeric(4,2) NOT NULL
);


--
-- TOC entry 229 (class 1259 OID 16673)
-- Name: catalog_of_works_job_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.catalog_of_works ALTER COLUMN job_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.catalog_of_works_job_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 240 (class 1259 OID 16760)
-- Name: clients; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.clients (
    client_id integer NOT NULL,
    client_user_id integer NOT NULL,
    client_name character varying(128) NOT NULL,
    client_surname character varying(128) NOT NULL,
    client_phone_number character varying(32) NOT NULL,
    client_email character varying(128) NOT NULL
);


--
-- TOC entry 239 (class 1259 OID 16759)
-- Name: clients_client_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.clients ALTER COLUMN client_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.clients_client_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 246 (class 1259 OID 16822)
-- Name: directory_of_statuses; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.directory_of_statuses (
    status_id integer NOT NULL,
    status_name character varying(128) NOT NULL,
    status_description text
);


--
-- TOC entry 245 (class 1259 OID 16821)
-- Name: directory_of_statuses_status_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.directory_of_statuses ALTER COLUMN status_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.directory_of_statuses_status_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 222 (class 1259 OID 16442)
-- Name: expenses; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.expenses (
    expenses_id integer NOT NULL,
    expenses_date date NOT NULL,
    expenses_category public.expenses_category_enum NOT NULL,
    tax_id integer,
    used_part_id integer,
    expenses_type public.expenses_type_enum NOT NULL,
    expenses_sum numeric(12,2) NOT NULL,
    CONSTRAINT chk_expenses_expenses_sum CHECK ((expenses_sum > (0)::numeric))
);


--
-- TOC entry 221 (class 1259 OID 16441)
-- Name: expenses_expenses_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.expenses ALTER COLUMN expenses_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.expenses_expenses_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 252 (class 1259 OID 16892)
-- Name: payment_journal; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.payment_journal (
    payment_id integer NOT NULL,
    payment_bill_id integer NOT NULL,
    payment_date date DEFAULT CURRENT_DATE NOT NULL,
    payment_amount numeric(12,2) NOT NULL,
    payment_method public.payment_method NOT NULL,
    CONSTRAINT chk_payment_amount CHECK ((payment_amount > (0)::numeric))
);


--
-- TOC entry 251 (class 1259 OID 16891)
-- Name: payment_journal_payment_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.payment_journal ALTER COLUMN payment_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.payment_journal_payment_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 256 (class 1259 OID 18244)
-- Name: proposed_parts; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.proposed_parts (
    proposed_part_id integer NOT NULL,
    proposed_parts_proposal_id integer NOT NULL,
    proposed_parts_used_part_id integer NOT NULL
);


--
-- TOC entry 255 (class 1259 OID 18243)
-- Name: proposed_parts_proposed_part_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.proposed_parts ALTER COLUMN proposed_part_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.proposed_parts_proposed_part_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 232 (class 1259 OID 16700)
-- Name: roles; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.roles (
    role_id integer NOT NULL,
    role_name public.role_name NOT NULL
);


--
-- TOC entry 231 (class 1259 OID 16699)
-- Name: roles_role_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.roles ALTER COLUMN role_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.roles_role_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 244 (class 1259 OID 16807)
-- Name: service_history; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.service_history (
    service_history_id integer NOT NULL,
    service_history_car_id integer NOT NULL,
    service_history_work_completion_date date NOT NULL,
    service_history_work_order_id integer NOT NULL,
    service_history_sum numeric(12,2) NOT NULL,
    CONSTRAINT chk_service_history_work_completion_date CHECK ((service_history_work_completion_date <= CURRENT_DATE))
);


--
-- TOC entry 243 (class 1259 OID 16806)
-- Name: service_history_service_history_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.service_history ALTER COLUMN service_history_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.service_history_service_history_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 234 (class 1259 OID 16710)
-- Name: specializations; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.specializations (
    specialization_id integer NOT NULL,
    specialization_name character varying(256) NOT NULL
);


--
-- TOC entry 233 (class 1259 OID 16709)
-- Name: specializations_specialization_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.specializations ALTER COLUMN specialization_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.specializations_specialization_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 224 (class 1259 OID 16459)
-- Name: suppliers; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.suppliers (
    supplier_id integer NOT NULL,
    supplier_name character varying(256) NOT NULL,
    supplier_contacts text NOT NULL
);


--
-- TOC entry 223 (class 1259 OID 16458)
-- Name: suppliers_supplier_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.suppliers ALTER COLUMN supplier_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.suppliers_supplier_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 220 (class 1259 OID 16400)
-- Name: taxes; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.taxes (
    tax_id integer NOT NULL,
    tax_name character varying(128) NOT NULL,
    tax_rate numeric(6,2) NOT NULL,
    tax_type public.tax_type_enum NOT NULL,
    CONSTRAINT chk_taxes_tax_rate CHECK ((tax_rate > (0)::numeric))
);


--
-- TOC entry 219 (class 1259 OID 16399)
-- Name: taxes_tax_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.taxes ALTER COLUMN tax_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.taxes_tax_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 226 (class 1259 OID 16484)
-- Name: used_parts; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.used_parts (
    used_part_id integer NOT NULL,
    used_parts_order_id integer,
    used_parts_supplier_id integer,
    used_part_name character varying(256) NOT NULL,
    used_part_article character varying(128) NOT NULL,
    used_part_quantity numeric(6,2) NOT NULL,
    used_part_unit_price numeric(10,2) NOT NULL,
    used_part_total_sum numeric(12,2) GENERATED ALWAYS AS ((used_part_quantity * used_part_unit_price)) STORED,
    CONSTRAINT chk_used_part_unit_price CHECK ((used_part_unit_price > (0)::numeric))
);


--
-- TOC entry 225 (class 1259 OID 16483)
-- Name: used_parts_used_part_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.used_parts ALTER COLUMN used_part_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.used_parts_used_part_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 238 (class 1259 OID 16736)
-- Name: users; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.users (
    user_id integer NOT NULL,
    user_role_id integer NOT NULL,
    user_login character varying(128) NOT NULL,
    user_password_hash character varying(256) NOT NULL
);


--
-- TOC entry 237 (class 1259 OID 16735)
-- Name: users_user_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.users ALTER COLUMN user_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.users_user_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 228 (class 1259 OID 16508)
-- Name: work_in_order; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.work_in_order (
    work_note_id integer NOT NULL,
    work_order_id integer NOT NULL,
    work_in_order_job_id integer NOT NULL,
    work_in_order_worker_id integer NOT NULL,
    work_in_order_time_spent numeric(4,2) NOT NULL,
    work_in_order_status_id integer DEFAULT 4 NOT NULL
);


--
-- TOC entry 227 (class 1259 OID 16507)
-- Name: work_in_order_work_note_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.work_in_order ALTER COLUMN work_note_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.work_in_order_work_note_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 248 (class 1259 OID 16832)
-- Name: work_orders; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.work_orders (
    work_order_id integer NOT NULL,
    work_order_status_id integer,
    work_order_car_id integer NOT NULL,
    work_order_creation_date date NOT NULL,
    work_in_order_priority public.priority DEFAULT 'Общий'::public.priority NOT NULL,
    CONSTRAINT chk_work_order_creation_date CHECK ((work_order_creation_date <= CURRENT_DATE))
);


--
-- TOC entry 247 (class 1259 OID 16831)
-- Name: work_orders_work_order_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.work_orders ALTER COLUMN work_order_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.work_orders_work_order_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 236 (class 1259 OID 16718)
-- Name: workers; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.workers (
    worker_id integer NOT NULL,
    worker_user_id integer NOT NULL,
    worker_specialization_id integer NOT NULL,
    worker_name character varying(128) NOT NULL,
    worker_surname character varying(128) NOT NULL,
    worker_hourly_rate numeric(10,2) NOT NULL,
    worker_phone character varying(32),
    worker_email character varying(128),
    CONSTRAINT workers_worker_hourly_rate_check CHECK ((worker_hourly_rate > (0)::numeric))
);


--
-- TOC entry 235 (class 1259 OID 16717)
-- Name: workers_worker_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.workers ALTER COLUMN worker_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.workers_worker_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 5027 (class 2606 OID 16800)
-- Name: cars cars_car_state_number_key; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.cars
    ADD CONSTRAINT cars_car_state_number_key UNIQUE (car_state_number);


--
-- TOC entry 5029 (class 2606 OID 16798)
-- Name: cars cars_car_vin_number_key; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.cars
    ADD CONSTRAINT cars_car_vin_number_key UNIQUE (car_vin_number);


--
-- TOC entry 5021 (class 2606 OID 16774)
-- Name: clients clients_client_email_key; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.clients
    ADD CONSTRAINT clients_client_email_key UNIQUE (client_email);


--
-- TOC entry 5023 (class 2606 OID 16772)
-- Name: clients clients_client_phone_number_key; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.clients
    ADD CONSTRAINT clients_client_phone_number_key UNIQUE (client_phone_number);


--
-- TOC entry 5043 (class 2606 OID 16922)
-- Name: additional_work_proposals pk_additional_work_proposals_proposal_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.additional_work_proposals
    ADD CONSTRAINT pk_additional_work_proposals_proposal_id PRIMARY KEY (proposal_id);


--
-- TOC entry 5039 (class 2606 OID 16868)
-- Name: bills pk_bills_bill_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.bills
    ADD CONSTRAINT pk_bills_bill_id PRIMARY KEY (bill_id);


--
-- TOC entry 5031 (class 2606 OID 16796)
-- Name: cars pk_cars_car_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.cars
    ADD CONSTRAINT pk_cars_car_id PRIMARY KEY (car_id);


--
-- TOC entry 5007 (class 2606 OID 16685)
-- Name: catalog_of_works pk_catalog_of_works_job_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.catalog_of_works
    ADD CONSTRAINT pk_catalog_of_works_job_id PRIMARY KEY (job_id);


--
-- TOC entry 5025 (class 2606 OID 16770)
-- Name: clients pk_clients_client_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.clients
    ADD CONSTRAINT pk_clients_client_id PRIMARY KEY (client_id);


--
-- TOC entry 5035 (class 2606 OID 16830)
-- Name: directory_of_statuses pk_directory_of_statuses_status_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.directory_of_statuses
    ADD CONSTRAINT pk_directory_of_statuses_status_id PRIMARY KEY (status_id);


--
-- TOC entry 4999 (class 2606 OID 16452)
-- Name: expenses pk_expenses_expenses_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.expenses
    ADD CONSTRAINT pk_expenses_expenses_id PRIMARY KEY (expenses_id);


--
-- TOC entry 5041 (class 2606 OID 16903)
-- Name: payment_journal pk_payment_journal_payment_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.payment_journal
    ADD CONSTRAINT pk_payment_journal_payment_id PRIMARY KEY (payment_id);


--
-- TOC entry 5045 (class 2606 OID 18251)
-- Name: proposed_parts pk_proposed_parts_proposed_part_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.proposed_parts
    ADD CONSTRAINT pk_proposed_parts_proposed_part_id PRIMARY KEY (proposed_part_id);


--
-- TOC entry 5009 (class 2606 OID 16706)
-- Name: roles pk_role_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT pk_role_id PRIMARY KEY (role_id);


--
-- TOC entry 5033 (class 2606 OID 16815)
-- Name: service_history pk_service_history_service_history_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.service_history
    ADD CONSTRAINT pk_service_history_service_history_id PRIMARY KEY (service_history_id);


--
-- TOC entry 5001 (class 2606 OID 16468)
-- Name: suppliers pk_suppliers_supplier_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.suppliers
    ADD CONSTRAINT pk_suppliers_supplier_id PRIMARY KEY (supplier_id);


--
-- TOC entry 4997 (class 2606 OID 16409)
-- Name: taxes pk_taxes_tax_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.taxes
    ADD CONSTRAINT pk_taxes_tax_id PRIMARY KEY (tax_id);


--
-- TOC entry 5003 (class 2606 OID 16496)
-- Name: used_parts pk_used_parts_used_part_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.used_parts
    ADD CONSTRAINT pk_used_parts_used_part_id PRIMARY KEY (used_part_id);


--
-- TOC entry 5017 (class 2606 OID 16744)
-- Name: users pk_users_user_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT pk_users_user_id PRIMARY KEY (user_id);


--
-- TOC entry 5005 (class 2606 OID 16517)
-- Name: work_in_order pk_work_in_order_work_note_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.work_in_order
    ADD CONSTRAINT pk_work_in_order_work_note_id PRIMARY KEY (work_note_id);


--
-- TOC entry 5037 (class 2606 OID 16840)
-- Name: work_orders pk_work_orders_work_order_id; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.work_orders
    ADD CONSTRAINT pk_work_orders_work_order_id PRIMARY KEY (work_order_id);


--
-- TOC entry 5011 (class 2606 OID 16708)
-- Name: roles roles_role_name_key; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_role_name_key UNIQUE (role_name);


--
-- TOC entry 5013 (class 2606 OID 16716)
-- Name: specializations specializations_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.specializations
    ADD CONSTRAINT specializations_pkey PRIMARY KEY (specialization_id);


--
-- TOC entry 5019 (class 2606 OID 16746)
-- Name: users users_user_login_key; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_user_login_key UNIQUE (user_login);


--
-- TOC entry 5015 (class 2606 OID 16729)
-- Name: workers workers_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.workers
    ADD CONSTRAINT workers_pkey PRIMARY KEY (worker_id);


--
-- TOC entry 5077 (class 2620 OID 19850)
-- Name: additional_work_proposals trg_additional_work_decision; Type: TRIGGER; Schema: public; Owner: -
--

CREATE TRIGGER trg_additional_work_decision BEFORE UPDATE ON public.additional_work_proposals FOR EACH ROW EXECUTE FUNCTION public.fn_additional_work_decision();


--
-- TOC entry 5074 (class 2620 OID 16946)
-- Name: bills trg_bill_total_sum; Type: TRIGGER; Schema: public; Owner: -
--

CREATE TRIGGER trg_bill_total_sum BEFORE INSERT ON public.bills FOR EACH ROW EXECUTE FUNCTION public.fn_bill_total_sum();


--
-- TOC entry 5076 (class 2620 OID 17086)
-- Name: payment_journal trg_payment_journal_pay_check; Type: TRIGGER; Schema: public; Owner: -
--

CREATE TRIGGER trg_payment_journal_pay_check AFTER INSERT ON public.payment_journal FOR EACH ROW EXECUTE FUNCTION public.fn_payment_journal_pay_check();


--
-- TOC entry 5075 (class 2620 OID 19831)
-- Name: bills trg_set_actual_closing_date; Type: TRIGGER; Schema: public; Owner: -
--

CREATE TRIGGER trg_set_actual_closing_date BEFORE UPDATE ON public.bills FOR EACH ROW EXECUTE FUNCTION public.fn_set_actual_closing_date();


--
-- TOC entry 5071 (class 2620 OID 19852)
-- Name: work_in_order trg_work_in_order_job_check; Type: TRIGGER; Schema: public; Owner: -
--

CREATE TRIGGER trg_work_in_order_job_check BEFORE INSERT OR UPDATE ON public.work_in_order FOR EACH ROW EXECUTE FUNCTION public.fn_work_in_order_job_check();


--
-- TOC entry 5073 (class 2620 OID 18599)
-- Name: work_orders trg_work_order_add_in_history; Type: TRIGGER; Schema: public; Owner: -
--

CREATE TRIGGER trg_work_order_add_in_history AFTER UPDATE ON public.work_orders FOR EACH ROW EXECUTE FUNCTION public.fn_work_order_add_in_history();


--
-- TOC entry 5072 (class 2620 OID 19849)
-- Name: work_in_order trg_work_order_status_inspection; Type: TRIGGER; Schema: public; Owner: -
--

CREATE TRIGGER trg_work_order_status_inspection AFTER UPDATE ON public.work_in_order FOR EACH ROW EXECUTE FUNCTION public.fn_work_order_status_inspection();


--
-- TOC entry 5061 (class 2606 OID 16869)
-- Name: bills fk_bill_for_work_order; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.bills
    ADD CONSTRAINT fk_bill_for_work_order FOREIGN KEY (bill_order_id) REFERENCES public.work_orders(work_order_id) ON DELETE CASCADE;


--
-- TOC entry 5062 (class 2606 OID 16874)
-- Name: bills fk_bill_has_status; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.bills
    ADD CONSTRAINT fk_bill_has_status FOREIGN KEY (bill_status_id) REFERENCES public.directory_of_statuses(status_id) ON DELETE SET NULL;


--
-- TOC entry 5056 (class 2606 OID 16801)
-- Name: cars fk_car_owned_by_cklient; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.cars
    ADD CONSTRAINT fk_car_owned_by_cklient FOREIGN KEY (car_owner_id) REFERENCES public.clients(client_id) ON DELETE CASCADE;


--
-- TOC entry 5064 (class 2606 OID 18515)
-- Name: additional_work_proposals fk_client_decision_status; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.additional_work_proposals
    ADD CONSTRAINT fk_client_decision_status FOREIGN KEY (client_decision) REFERENCES public.directory_of_statuses(status_id);


--
-- TOC entry 5055 (class 2606 OID 16775)
-- Name: clients fk_client_has_user_account; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.clients
    ADD CONSTRAINT fk_client_has_user_account FOREIGN KEY (client_user_id) REFERENCES public.users(user_id) ON DELETE CASCADE;


--
-- TOC entry 5046 (class 2606 OID 16502)
-- Name: expenses fk_part_expenses; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.expenses
    ADD CONSTRAINT fk_part_expenses FOREIGN KEY (used_part_id) REFERENCES public.used_parts(used_part_id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- TOC entry 5063 (class 2606 OID 16904)
-- Name: payment_journal fk_payment_for_bill; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.payment_journal
    ADD CONSTRAINT fk_payment_for_bill FOREIGN KEY (payment_bill_id) REFERENCES public.bills(bill_id) ON DELETE CASCADE;


--
-- TOC entry 5065 (class 2606 OID 16923)
-- Name: additional_work_proposals fk_proposal_order; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.additional_work_proposals
    ADD CONSTRAINT fk_proposal_order FOREIGN KEY (order_id) REFERENCES public.work_orders(work_order_id) ON DELETE CASCADE;


--
-- TOC entry 5066 (class 2606 OID 18520)
-- Name: additional_work_proposals fk_proposal_status; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.additional_work_proposals
    ADD CONSTRAINT fk_proposal_status FOREIGN KEY (proposal_status) REFERENCES public.directory_of_statuses(status_id);


--
-- TOC entry 5067 (class 2606 OID 16933)
-- Name: additional_work_proposals fk_proposed_by_worker; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.additional_work_proposals
    ADD CONSTRAINT fk_proposed_by_worker FOREIGN KEY (proposed_by_worker_id) REFERENCES public.workers(worker_id) ON DELETE CASCADE;


--
-- TOC entry 5069 (class 2606 OID 18252)
-- Name: proposed_parts fk_proposed_parts_additional_work_proposals; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.proposed_parts
    ADD CONSTRAINT fk_proposed_parts_additional_work_proposals FOREIGN KEY (proposed_parts_proposal_id) REFERENCES public.additional_work_proposals(proposal_id) ON DELETE CASCADE;


--
-- TOC entry 5070 (class 2606 OID 18257)
-- Name: proposed_parts fk_proposed_parts_used_parts; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.proposed_parts
    ADD CONSTRAINT fk_proposed_parts_used_parts FOREIGN KEY (proposed_parts_used_part_id) REFERENCES public.used_parts(used_part_id) ON DELETE CASCADE;


--
-- TOC entry 5068 (class 2606 OID 16928)
-- Name: additional_work_proposals fk_proposed_work; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.additional_work_proposals
    ADD CONSTRAINT fk_proposed_work FOREIGN KEY (proposed_work_id) REFERENCES public.catalog_of_works(job_id) ON DELETE CASCADE;


--
-- TOC entry 5057 (class 2606 OID 16816)
-- Name: service_history fk_service_history_for_car; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.service_history
    ADD CONSTRAINT fk_service_history_for_car FOREIGN KEY (service_history_car_id) REFERENCES public.cars(car_id) ON DELETE CASCADE;


--
-- TOC entry 5058 (class 2606 OID 18593)
-- Name: service_history fk_service_history_work_order_id_has_work_order_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.service_history
    ADD CONSTRAINT fk_service_history_work_order_id_has_work_order_id FOREIGN KEY (service_history_work_order_id) REFERENCES public.work_orders(work_order_id) ON DELETE CASCADE;


--
-- TOC entry 5048 (class 2606 OID 16497)
-- Name: used_parts fk_supplier_parts; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.used_parts
    ADD CONSTRAINT fk_supplier_parts FOREIGN KEY (used_parts_supplier_id) REFERENCES public.suppliers(supplier_id) ON DELETE SET NULL;


--
-- TOC entry 5047 (class 2606 OID 16453)
-- Name: expenses fk_tax_expenses; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.expenses
    ADD CONSTRAINT fk_tax_expenses FOREIGN KEY (tax_id) REFERENCES public.taxes(tax_id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- TOC entry 5049 (class 2606 OID 16851)
-- Name: used_parts fk_used_in_order; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.used_parts
    ADD CONSTRAINT fk_used_in_order FOREIGN KEY (used_parts_order_id) REFERENCES public.work_orders(work_order_id) ON DELETE CASCADE;


--
-- TOC entry 5054 (class 2606 OID 16747)
-- Name: users fk_user_has_role; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT fk_user_has_role FOREIGN KEY (user_role_id) REFERENCES public.roles(role_id) ON DELETE RESTRICT;


--
-- TOC entry 5050 (class 2606 OID 19843)
-- Name: work_in_order fk_work_in_order_status; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.work_in_order
    ADD CONSTRAINT fk_work_in_order_status FOREIGN KEY (work_in_order_status_id) REFERENCES public.directory_of_statuses(status_id);


--
-- TOC entry 5059 (class 2606 OID 16846)
-- Name: work_orders fk_work_order_for_car; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.work_orders
    ADD CONSTRAINT fk_work_order_for_car FOREIGN KEY (work_order_car_id) REFERENCES public.cars(car_id) ON DELETE CASCADE;


--
-- TOC entry 5060 (class 2606 OID 16841)
-- Name: work_orders fk_work_order_has_status; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.work_orders
    ADD CONSTRAINT fk_work_order_has_status FOREIGN KEY (work_order_status_id) REFERENCES public.directory_of_statuses(status_id) ON DELETE SET NULL;


--
-- TOC entry 5051 (class 2606 OID 16686)
-- Name: work_in_order fk_work_performed; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.work_in_order
    ADD CONSTRAINT fk_work_performed FOREIGN KEY (work_in_order_job_id) REFERENCES public.catalog_of_works(job_id) ON DELETE CASCADE;


--
-- TOC entry 5052 (class 2606 OID 16754)
-- Name: workers fk_worker_has_user_account; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.workers
    ADD CONSTRAINT fk_worker_has_user_account FOREIGN KEY (worker_user_id) REFERENCES public.users(user_id) ON DELETE CASCADE;


--
-- TOC entry 5053 (class 2606 OID 16730)
-- Name: workers fk_worker_specialization; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.workers
    ADD CONSTRAINT fk_worker_specialization FOREIGN KEY (worker_specialization_id) REFERENCES public.specializations(specialization_id) ON DELETE CASCADE;


-- Completed on 2025-10-09 00:58:38

--
-- PostgreSQL database dump complete
--

\unrestrict xkTRIMwGJ28BttYE88qhYi7jTl0EHP7ONLWaRt3LyUIJWFzu0agl5JEpDkq448Z

