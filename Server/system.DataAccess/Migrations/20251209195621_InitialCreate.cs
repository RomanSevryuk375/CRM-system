using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRMSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bill_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bill_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "car_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_car_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notification_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notification_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "part_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_part_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shifts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    start_at = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    end_at = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shifts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "specializations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specializations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "storage_cells",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rack = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    shelf = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_storage_cells", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    contacts = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_suppliers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "taxes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    rate = table.Column<decimal>(type: "numeric(2,2)", nullable: false),
                    type = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_taxes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "work_in_order_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_work_in_order_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "work_proposal_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_work_proposal_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "works_catalog",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    category = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    standard_time = table.Column<decimal>(type: "numeric(4,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_works_catalog", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "parts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    oem_article = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    manufacturer_article = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    internal_article = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    manufacturer = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    applicability = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_parts", x => x.id);
                    table.ForeignKey(
                        name: "fk_parts_part_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "part_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<int>(type: "integer", maxLength: 64, nullable: false),
                    login = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "supplies",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    supplier_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supplies", x => x.id);
                    table.ForeignKey(
                        name: "fk_supplies_suppliers_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    part_id = table.Column<long>(type: "bigint", nullable: false),
                    cell_id = table.Column<int>(type: "integer", nullable: false),
                    purchase_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    selling_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_positions", x => x.id);
                    table.ForeignKey(
                        name: "fk_positions_parts_part_id",
                        column: x => x.part_id,
                        principalTable: "parts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_positions_storage_cells_cell_id",
                        column: x => x.cell_id,
                        principalTable: "storage_cells",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    surname = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_clients_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "workers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    surname = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    hourly_rate = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    phone_number = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workers", x => x.id);
                    table.ForeignKey(
                        name: "fk_workers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "supply_sets",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    supply_id = table.Column<long>(type: "bigint", nullable: false),
                    position_id = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    purchase_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supply_sets", x => x.id);
                    table.ForeignKey(
                        name: "fk_supply_sets_positions_position_id",
                        column: x => x.position_id,
                        principalTable: "positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_supply_sets_supplies_supply_id",
                        column: x => x.supply_id,
                        principalTable: "supplies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    owner_id = table.Column<long>(type: "bigint", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    brand = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    model = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    year_of_manufacture = table.Column<int>(type: "integer", nullable: false),
                    vin_number = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    state_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    mileage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cars", x => x.id);
                    table.ForeignKey(
                        name: "fk_cars_car_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "car_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cars_clients_owner_id",
                        column: x => x.owner_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "absences",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    worker_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_absences", x => x.id);
                    table.ForeignKey(
                        name: "fk_absences_workers_worker_id",
                        column: x => x.worker_id,
                        principalTable: "workers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    worker_id = table.Column<int>(type: "integer", nullable: false),
                    shift_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schedules", x => x.id);
                    table.ForeignKey(
                        name: "fk_schedules_shifts_shift_id",
                        column: x => x.shift_id,
                        principalTable: "shifts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_schedules_workers_worker_id",
                        column: x => x.worker_id,
                        principalTable: "workers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "skills",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    worker_id = table.Column<int>(type: "integer", nullable: false),
                    specialization_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_skills", x => x.id);
                    table.ForeignKey(
                        name: "fk_skills_specializations_specialization_id",
                        column: x => x.specialization_id,
                        principalTable: "specializations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_skills_workers_worker_id",
                        column: x => x.worker_id,
                        principalTable: "workers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<long>(type: "bigint", nullable: false),
                    car_id = table.Column<long>(type: "bigint", nullable: false),
                    type = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    message = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    send_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notifications", x => x.id);
                    table.ForeignKey(
                        name: "fk_notifications_cars_car_id",
                        column: x => x.car_id,
                        principalTable: "cars",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_notifications_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_notifications_notifications_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "notification_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    car_id = table.Column<long>(type: "bigint", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    priority = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_work_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_work_orders_cars_car_id",
                        column: x => x.car_id,
                        principalTable: "cars",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_work_orders_order_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "order_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "acceptances",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    worker_id = table.Column<int>(type: "integer", nullable: false),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    mileage = table.Column<int>(type: "integer", nullable: false),
                    fuel_level = table.Column<int>(type: "integer", nullable: false),
                    external_defects = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    internal_defects = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    client_sign = table.Column<bool>(type: "boolean", nullable: true),
                    worker_sign = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_acceptances", x => x.id);
                    table.ForeignKey(
                        name: "fk_acceptances_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "work_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_acceptances_workers_worker_id",
                        column: x => x.worker_id,
                        principalTable: "workers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "additional_work_proposals",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    job_id = table.Column<long>(type: "bigint", nullable: false),
                    worker_id = table.Column<int>(type: "integer", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_additional_work_proposals", x => x.id);
                    table.ForeignKey(
                        name: "fk_additional_work_proposals_work_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "work_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_additional_work_proposals_work_proposal_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "work_proposal_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_additional_work_proposals_workers_worker_id",
                        column: x => x.worker_id,
                        principalTable: "workers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_additional_work_proposals_works_catalog_job_id",
                        column: x => x.job_id,
                        principalTable: "works_catalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attachments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    worker_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attachments", x => x.id);
                    table.ForeignKey(
                        name: "fk_attachments_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "work_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_attachments_workers_worker_id",
                        column: x => x.worker_id,
                        principalTable: "workers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bills",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    actual_bill_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bills", x => x.id);
                    table.ForeignKey(
                        name: "fk_bills_bills_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "bill_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_bills_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "work_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "guarantees",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    date_start = table.Column<DateOnly>(type: "date", nullable: false),
                    date_end = table.Column<DateOnly>(type: "date", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    terms = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_guarantees", x => x.id);
                    table.ForeignKey(
                        name: "fk_guarantees_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "work_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "acceptence_imgs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    acceptance_id = table.Column<long>(type: "bigint", nullable: false),
                    file_path = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_acceptence_imgs", x => x.id);
                    table.ForeignKey(
                        name: "fk_acceptence_imgs_acceptances_acceptance_id",
                        column: x => x.acceptance_id,
                        principalTable: "acceptances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "part_sets",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    position_id = table.Column<long>(type: "bigint", nullable: false),
                    proposal_id = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    sold_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_part_sets", x => x.id);
                    table.ForeignKey(
                        name: "fk_part_sets_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "work_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_part_sets_positions_position_id",
                        column: x => x.position_id,
                        principalTable: "positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_part_sets_work_proposals_proposal_id",
                        column: x => x.proposal_id,
                        principalTable: "additional_work_proposals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "works_in_order",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    job_id = table.Column<long>(type: "bigint", nullable: false),
                    worker_id = table.Column<int>(type: "integer", nullable: false),
                    time_spent = table.Column<decimal>(type: "numeric(4,2)", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    work_proposal_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_works_in_order", x => x.id);
                    table.ForeignKey(
                        name: "fk_works_in_order_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "work_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_works_in_order_work_in_order_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "work_in_order_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_works_in_order_work_proposals_work_proposal_id",
                        column: x => x.work_proposal_id,
                        principalTable: "additional_work_proposals",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_works_in_order_workers_worker_id",
                        column: x => x.worker_id,
                        principalTable: "workers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_works_in_order_works_catalog_job_id",
                        column: x => x.job_id,
                        principalTable: "works_catalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "attachment_imgs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    attachment_id = table.Column<long>(type: "bigint", nullable: false),
                    file_path = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attachment_imgs", x => x.id);
                    table.ForeignKey(
                        name: "fk_attachment_imgs_attachments_attachment_id",
                        column: x => x.attachment_id,
                        principalTable: "attachments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment_journal",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bill_id = table.Column<long>(type: "bigint", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    method = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_journal", x => x.id);
                    table.ForeignKey(
                        name: "fk_payment_journal_bills_bill_id",
                        column: x => x.bill_id,
                        principalTable: "bills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "expenses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tax_id = table.Column<int>(type: "integer", nullable: true),
                    part_set_id = table.Column<long>(type: "bigint", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    expense_type = table.Column<string>(type: "text", nullable: false),
                    sum = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_expenses", x => x.id);
                    table.ForeignKey(
                        name: "fk_expenses_part_sets_part_set_id",
                        column: x => x.part_set_id,
                        principalTable: "part_sets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_expenses_taxes_tax_id",
                        column: x => x.tax_id,
                        principalTable: "taxes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "bill_statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Оплачен" },
                    { 2, "Не оплачен" },
                    { 3, "Частично оплачен" }
                });

            migrationBuilder.InsertData(
                table: "car_statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "В работе" },
                    { 2, "Не в работе" }
                });

            migrationBuilder.InsertData(
                table: "notification_statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Отправлено" },
                    { 2, "Прочитано" }
                });

            migrationBuilder.InsertData(
                table: "order_statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "В ожидании" },
                    { 2, "Принят" },
                    { 3, "Диагнстика" },
                    { 4, "Закрыт" },
                    { 5, "В работе" },
                    { 6, "Завершен" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Менеджер" },
                    { 2, "Клиент" },
                    { 3, "Работник" }
                });

            migrationBuilder.InsertData(
                table: "specializations",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "автомеханик" },
                    { 2, "моторист" },
                    { 3, "специалист по коробкам передач" },
                    { 4, "ходовик" },
                    { 5, "специалист по тормозным системам" },
                    { 6, "автоэлектрик" },
                    { 7, "диагност" },
                    { 8, "кузовщик" },
                    { 9, "маляр" }
                });

            migrationBuilder.InsertData(
                table: "work_in_order_statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "В работе" },
                    { 2, "В ожидании" },
                    { 3, "завершен" }
                });

            migrationBuilder.InsertData(
                table: "work_proposal_statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "В ожидании" },
                    { 2, "Принято" },
                    { 3, "Отклонено" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_absences_worker_id",
                table: "absences",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "ix_acceptances_order_id",
                table: "acceptances",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_acceptances_worker_id",
                table: "acceptances",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "ix_acceptence_imgs_acceptance_id",
                table: "acceptence_imgs",
                column: "acceptance_id");

            migrationBuilder.CreateIndex(
                name: "ix_additional_work_proposals_job_id",
                table: "additional_work_proposals",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "ix_additional_work_proposals_order_id",
                table: "additional_work_proposals",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_additional_work_proposals_status_id",
                table: "additional_work_proposals",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_additional_work_proposals_worker_id",
                table: "additional_work_proposals",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "ix_attachment_imgs_attachment_id",
                table: "attachment_imgs",
                column: "attachment_id");

            migrationBuilder.CreateIndex(
                name: "ix_attachments_order_id",
                table: "attachments",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_attachments_worker_id",
                table: "attachments",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "ix_bills_order_id",
                table: "bills",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_bills_status_id",
                table: "bills",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_cars_owner_id",
                table: "cars",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_cars_state_number",
                table: "cars",
                column: "state_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cars_status_id",
                table: "cars",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_cars_vin_number",
                table: "cars",
                column: "vin_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_clients_email",
                table: "clients",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_clients_phone_number",
                table: "clients",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_clients_user_id",
                table: "clients",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_expenses_part_set_id",
                table: "expenses",
                column: "part_set_id");

            migrationBuilder.CreateIndex(
                name: "ix_expenses_tax_id",
                table: "expenses",
                column: "tax_id");

            migrationBuilder.CreateIndex(
                name: "ix_guarantees_order_id",
                table: "guarantees",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications_car_id",
                table: "notifications",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications_client_id",
                table: "notifications",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications_status_id",
                table: "notifications",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_part_sets_order_id",
                table: "part_sets",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_part_sets_position_id",
                table: "part_sets",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "ix_part_sets_proposal_id",
                table: "part_sets",
                column: "proposal_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_parts_category_id",
                table: "parts",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_journal_bill_id",
                table: "payment_journal",
                column: "bill_id");

            migrationBuilder.CreateIndex(
                name: "ix_positions_cell_id",
                table: "positions",
                column: "cell_id");

            migrationBuilder.CreateIndex(
                name: "ix_positions_part_id",
                table: "positions",
                column: "part_id");

            migrationBuilder.CreateIndex(
                name: "ix_schedules_shift_id",
                table: "schedules",
                column: "shift_id");

            migrationBuilder.CreateIndex(
                name: "ix_schedules_worker_id",
                table: "schedules",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "ix_skills_specialization_id",
                table: "skills",
                column: "specialization_id");

            migrationBuilder.CreateIndex(
                name: "ix_skills_worker_id",
                table: "skills",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "ix_supplies_supplier_id",
                table: "supplies",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "ix_supply_sets_position_id",
                table: "supply_sets",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "ix_supply_sets_supply_id",
                table: "supply_sets",
                column: "supply_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_login",
                table: "users",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_work_orders_car_id",
                table: "work_orders",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_work_orders_status_id",
                table: "work_orders",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_workers_email",
                table: "workers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_workers_phone_number",
                table: "workers",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_workers_user_id",
                table: "workers",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_works_in_order_job_id",
                table: "works_in_order",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "ix_works_in_order_order_id",
                table: "works_in_order",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_works_in_order_status_id",
                table: "works_in_order",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_works_in_order_work_proposal_id",
                table: "works_in_order",
                column: "work_proposal_id");

            migrationBuilder.CreateIndex(
                name: "ix_works_in_order_worker_id",
                table: "works_in_order",
                column: "worker_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "absences");

            migrationBuilder.DropTable(
                name: "acceptence_imgs");

            migrationBuilder.DropTable(
                name: "attachment_imgs");

            migrationBuilder.DropTable(
                name: "expenses");

            migrationBuilder.DropTable(
                name: "guarantees");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "payment_journal");

            migrationBuilder.DropTable(
                name: "schedules");

            migrationBuilder.DropTable(
                name: "skills");

            migrationBuilder.DropTable(
                name: "supply_sets");

            migrationBuilder.DropTable(
                name: "works_in_order");

            migrationBuilder.DropTable(
                name: "acceptances");

            migrationBuilder.DropTable(
                name: "attachments");

            migrationBuilder.DropTable(
                name: "part_sets");

            migrationBuilder.DropTable(
                name: "taxes");

            migrationBuilder.DropTable(
                name: "notification_statuses");

            migrationBuilder.DropTable(
                name: "bills");

            migrationBuilder.DropTable(
                name: "shifts");

            migrationBuilder.DropTable(
                name: "specializations");

            migrationBuilder.DropTable(
                name: "supplies");

            migrationBuilder.DropTable(
                name: "work_in_order_statuses");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.DropTable(
                name: "additional_work_proposals");

            migrationBuilder.DropTable(
                name: "bill_statuses");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "parts");

            migrationBuilder.DropTable(
                name: "storage_cells");

            migrationBuilder.DropTable(
                name: "work_orders");

            migrationBuilder.DropTable(
                name: "work_proposal_statuses");

            migrationBuilder.DropTable(
                name: "workers");

            migrationBuilder.DropTable(
                name: "works_catalog");

            migrationBuilder.DropTable(
                name: "part_categories");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "order_statuses");

            migrationBuilder.DropTable(
                name: "car_statuses");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
