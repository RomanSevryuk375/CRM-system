using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Ordering.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialOrderingMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ordering");

            migrationBuilder.CreateTable(
                name: "attachments",
                schema: "ordering",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    uploaded_by = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    file_path = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    content_type = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    version = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attachments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inbox_messages",
                schema: "ordering",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "ordering",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    car_id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    started_at = table.Column<DateOnly>(type: "date", nullable: false),
                    planned_finish_date = table.Column<DateOnly>(type: "date", nullable: true),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    version = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                schema: "ordering",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "jsonb", nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    error = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "service_catalog_items",
                schema: "ordering",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    category = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    standard_time = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    version = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_service_catalog_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_inspections",
                schema: "ordering",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    mileage = table.Column<int>(type: "integer", nullable: false),
                    fuel_level = table.Column<int>(type: "integer", nullable: false),
                    cleanliness_level = table.Column<int>(type: "integer", nullable: false),
                    personal_belongings = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    dashboard_warnings = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    external_defects = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    internal_defects = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    has_wheel_nut_key = table.Column<bool>(type: "boolean", nullable: false),
                    has_service_book = table.Column<bool>(type: "boolean", nullable: false),
                    client_sign = table.Column<bool>(type: "boolean", nullable: false),
                    worker_sign = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    version = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehicle_inspections", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "work_proposals",
                schema: "ordering",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    job_id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    version = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_work_proposals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order_parts",
                schema: "ordering",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    part_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_proposed = table.Column<bool>(type: "boolean", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    sold_price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_parts", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_parts_orders_order_id",
                        column: x => x.order_id,
                        principalSchema: "ordering",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_works",
                schema: "ordering",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    job_id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: true),
                    estimated_hours = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    time_spent = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    is_proposed = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    hourly_rate = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    fixed_price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    total_cost = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_works", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_works_orders_order_id",
                        column: x => x.order_id,
                        principalSchema: "ordering",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_inspection_images",
                schema: "ordering",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    inspection_id = table.Column<Guid>(type: "uuid", nullable: false),
                    path = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehicle_inspection_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_vehicle_inspection_images_vehicle_inspections_inspection_id",
                        column: x => x.inspection_id,
                        principalSchema: "ordering",
                        principalTable: "vehicle_inspections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_guarantees",
                schema: "ordering",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_part_id = table.Column<Guid>(type: "uuid", nullable: true),
                    order_work_id = table.Column<Guid>(type: "uuid", nullable: true),
                    date_start = table.Column<DateOnly>(type: "date", nullable: false),
                    date_end = table.Column<DateOnly>(type: "date", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    terms = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_guarantees", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_guarantees_order_parts_order_part_id",
                        column: x => x.order_part_id,
                        principalSchema: "ordering",
                        principalTable: "order_parts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_order_guarantees_order_works_order_work_id",
                        column: x => x.order_work_id,
                        principalSchema: "ordering",
                        principalTable: "order_works",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_order_guarantees_orders_order_id",
                        column: x => x.order_id,
                        principalSchema: "ordering",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_attachments_order_id",
                schema: "ordering",
                table: "attachments",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_attachments_uploaded_by",
                schema: "ordering",
                table: "attachments",
                column: "uploaded_by");

            migrationBuilder.CreateIndex(
                name: "ix_order_guarantees_order_id",
                schema: "ordering",
                table: "order_guarantees",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_guarantees_order_part_id",
                schema: "ordering",
                table: "order_guarantees",
                column: "order_part_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_guarantees_order_work_id",
                schema: "ordering",
                table: "order_guarantees",
                column: "order_work_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_parts_order_id",
                schema: "ordering",
                table: "order_parts",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_parts_part_id",
                schema: "ordering",
                table: "order_parts",
                column: "part_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_works_job_id",
                schema: "ordering",
                table: "order_works",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_works_order_id",
                schema: "ordering",
                table: "order_works",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_works_status",
                schema: "ordering",
                table: "order_works",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_order_works_worker_id",
                schema: "ordering",
                table: "order_works",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_car_id",
                schema: "ordering",
                table: "orders",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_started_at",
                schema: "ordering",
                table: "orders",
                column: "started_at");

            migrationBuilder.CreateIndex(
                name: "ix_orders_status",
                schema: "ordering",
                table: "orders",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_orders_worker_id",
                schema: "ordering",
                table: "orders",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "ix_outbox_messages_occurred_on_utc",
                schema: "ordering",
                table: "outbox_messages",
                column: "occurred_on_utc");

            migrationBuilder.CreateIndex(
                name: "ix_service_catalog_items_category",
                schema: "ordering",
                table: "service_catalog_items",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "ix_service_catalog_items_title",
                schema: "ordering",
                table: "service_catalog_items",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "ix_vehicle_inspection_images_inspection_id",
                schema: "ordering",
                table: "vehicle_inspection_images",
                column: "inspection_id");

            migrationBuilder.CreateIndex(
                name: "ix_vehicle_inspections_order_id",
                schema: "ordering",
                table: "vehicle_inspections",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_vehicle_inspections_status",
                schema: "ordering",
                table: "vehicle_inspections",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_vehicle_inspections_worker_id",
                schema: "ordering",
                table: "vehicle_inspections",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "ix_work_proposals_job_id",
                schema: "ordering",
                table: "work_proposals",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "ix_work_proposals_order_id",
                schema: "ordering",
                table: "work_proposals",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_work_proposals_status",
                schema: "ordering",
                table: "work_proposals",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_work_proposals_worker_id",
                schema: "ordering",
                table: "work_proposals",
                column: "worker_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attachments",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "inbox_messages",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "order_guarantees",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "outbox_messages",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "service_catalog_items",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "vehicle_inspection_images",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "work_proposals",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "order_parts",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "order_works",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "vehicle_inspections",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "ordering");
        }
    }
}
