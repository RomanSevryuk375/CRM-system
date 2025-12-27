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
                name: "absence_types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_absence_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bill_statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bill_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "car_statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "expense_types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expense_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "notification_statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "notification_types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "order_priorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "order_statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "part_categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payment_methods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_methods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    StartAt = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndAt = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "storage_cells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Rack = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Shelf = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storage_cells", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Contacts = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tax_types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tax_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "work_in_order_statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_in_order_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "work_proposal_statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_proposal_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "works_catalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Category = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    StandardTime = table.Column<decimal>(type: "numeric(4,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works_catalog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "parts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    OEMArticle = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ManufacturerArticle = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    InternalArticle = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Manufacturer = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Applicability = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_parts_part_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "part_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    Login = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "supplies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupplierId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_supplies_suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "taxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Rate = table.Column<decimal>(type: "numeric(2,2)", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_taxes_tax_types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "tax_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartId = table.Column<long>(type: "bigint", nullable: false),
                    CellId = table.Column<int>(type: "integer", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_positions_parts_PartId",
                        column: x => x.PartId,
                        principalTable: "parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_positions_storage_cells_CellId",
                        column: x => x.CellId,
                        principalTable: "storage_cells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Surname = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_clients_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Surname = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    HourlyRate = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workers_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "supply_sets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupplyId = table.Column<long>(type: "bigint", nullable: false),
                    PositionId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supply_sets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_supply_sets_positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_supply_sets_supplies_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "supplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    Brand = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Model = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    YearOfManufacture = table.Column<int>(type: "integer", nullable: false),
                    VinNumber = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    StateNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Mileage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cars_car_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "car_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cars_clients_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "absences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkerId = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_absences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_absences_absence_types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "absence_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_absences_workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkerId = table.Column<int>(type: "integer", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_schedules_shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_schedules_workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkerId = table.Column<int>(type: "integer", nullable: false),
                    SpecializationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_skills_specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_skills_workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    CarId = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    SendAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notifications_cars_CarId",
                        column: x => x.CarId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_notifications_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_notifications_notification_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "notification_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_notifications_notification_types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "notification_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "work_orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CarId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    PriorityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_work_orders_cars_CarId",
                        column: x => x.CarId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_work_orders_order_priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "order_priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_work_orders_order_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "order_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "acceptances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    WorkerId = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Mileage = table.Column<int>(type: "integer", nullable: false),
                    FuelLevel = table.Column<int>(type: "integer", nullable: false),
                    ExternalDefects = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    InternalDefects = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ClientSign = table.Column<bool>(type: "boolean", nullable: true),
                    WorkerSign = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acceptances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acceptances_work_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "work_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acceptances_workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "additional_work_proposals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    WorkerId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_additional_work_proposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_additional_work_proposals_work_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "work_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_additional_work_proposals_work_proposal_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "work_proposal_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_additional_work_proposals_workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_additional_work_proposals_works_catalog_JobId",
                        column: x => x.JobId,
                        principalTable: "works_catalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attachments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    WorkerId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attachments_work_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "work_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_attachments_workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ActualBillDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bills_bill_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "bill_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bills_work_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "work_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "guarantees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    DateStart = table.Column<DateOnly>(type: "date", nullable: false),
                    DateEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Terms = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guarantees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_guarantees_work_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "work_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "acceptance_imgs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AcceptanceId = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acceptance_imgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acceptance_imgs_acceptances_AcceptanceId",
                        column: x => x.AcceptanceId,
                        principalTable: "acceptances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "part_sets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: true),
                    PositionId = table.Column<long>(type: "bigint", nullable: false),
                    ProposalId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    SoldPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part_sets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_part_sets_additional_work_proposals_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "additional_work_proposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_part_sets_positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_part_sets_work_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "work_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "works_in_order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    WorkerId = table.Column<int>(type: "integer", nullable: false),
                    TimeSpent = table.Column<decimal>(type: "numeric(4,2)", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    WorkProposalId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works_in_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_works_in_order_additional_work_proposals_WorkProposalId",
                        column: x => x.WorkProposalId,
                        principalTable: "additional_work_proposals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_works_in_order_work_in_order_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "work_in_order_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_works_in_order_work_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "work_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_works_in_order_workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_works_in_order_works_catalog_JobId",
                        column: x => x.JobId,
                        principalTable: "works_catalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "attachment_imgs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AttachmentId = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attachment_imgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attachment_imgs_attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment_journal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BillId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    MethodId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_journal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payment_journal_bills_BillId",
                        column: x => x.BillId,
                        principalTable: "bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_payment_journal_payment_methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "payment_methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "expenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaxId = table.Column<int>(type: "integer", nullable: true),
                    PartSetId = table.Column<long>(type: "bigint", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Category = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ExpenseTypeId = table.Column<int>(type: "integer", nullable: false),
                    Sum = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_expenses_expense_types_ExpenseTypeId",
                        column: x => x.ExpenseTypeId,
                        principalTable: "expense_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_expenses_part_sets_PartSetId",
                        column: x => x.PartSetId,
                        principalTable: "part_sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_expenses_taxes_TaxId",
                        column: x => x.TaxId,
                        principalTable: "taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "absence_types",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Больничный" },
                    { 2, "Прогул" },
                    { 3, "Отпуск" },
                    { 4, "Отгул" }
                });

            migrationBuilder.InsertData(
                table: "bill_statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Оплачен" },
                    { 2, "Не оплачен" },
                    { 3, "Частично оплачен" }
                });

            migrationBuilder.InsertData(
                table: "car_statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "В работе" },
                    { 2, "Не в работе" }
                });

            migrationBuilder.InsertData(
                table: "expense_types",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Запчасти и материалы" },
                    { 2, "Аренда и коммунальные услуги" },
                    { 3, "Оборудование и инструмент" },
                    { 4, "IT и связь" },
                    { 5, "Маркетинг и реклама" },
                    { 6, "Документация и лицензии" },
                    { 7, "Логистика и транспорт" },
                    { 8, "Офисные и хозяйственные расходы" },
                    { 9, "Финансовые расходы" }
                });

            migrationBuilder.InsertData(
                table: "notification_statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Отправлено" },
                    { 2, "Прочитано" }
                });

            migrationBuilder.InsertData(
                table: "notification_types",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Работа с заказами" },
                    { 2, "Платежи и финансы" },
                    { 3, "Запчасти и склад" },
                    { 4, "Планирование и обслуживание" },
                    { 5, "Системные уведомления" },
                    { 6, "Коммуникация с клиентами" }
                });

            migrationBuilder.InsertData(
                table: "order_priorities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Низкий" },
                    { 2, "Обычый" },
                    { 3, "Высокий" }
                });

            migrationBuilder.InsertData(
                table: "order_statuses",
                columns: new[] { "Id", "Name" },
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
                table: "payment_methods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Картой" },
                    { 2, "Наличными" },
                    { 3, "ЕРИП" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Менеджер" },
                    { 2, "Клиент" },
                    { 3, "Работник" }
                });

            migrationBuilder.InsertData(
                table: "specializations",
                columns: new[] { "Id", "Name" },
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
                table: "tax_types",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Налог на прибыль" },
                    { 2, "НДС" },
                    { 3, "Налог на недвижимость" },
                    { 4, "Земельный налог" },
                    { 5, "Социальные взносы" },
                    { 6, "Экологический налог" },
                    { 7, "Местные сборы" }
                });

            migrationBuilder.InsertData(
                table: "work_in_order_statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "В работе" },
                    { 2, "В ожидании" },
                    { 3, "завершен" }
                });

            migrationBuilder.InsertData(
                table: "work_proposal_statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "В ожидании" },
                    { 2, "Принято" },
                    { 3, "Отклонено" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_absences_TypeId",
                table: "absences",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_absences_WorkerId",
                table: "absences",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_acceptance_imgs_AcceptanceId",
                table: "acceptance_imgs",
                column: "AcceptanceId");

            migrationBuilder.CreateIndex(
                name: "IX_acceptances_OrderId",
                table: "acceptances",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_acceptances_WorkerId",
                table: "acceptances",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_additional_work_proposals_JobId",
                table: "additional_work_proposals",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_additional_work_proposals_OrderId",
                table: "additional_work_proposals",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_additional_work_proposals_StatusId",
                table: "additional_work_proposals",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_additional_work_proposals_WorkerId",
                table: "additional_work_proposals",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_attachment_imgs_AttachmentId",
                table: "attachment_imgs",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_attachments_OrderId",
                table: "attachments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_attachments_WorkerId",
                table: "attachments",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_bills_OrderId",
                table: "bills",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_bills_StatusId",
                table: "bills",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_OwnerId",
                table: "cars",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_StateNumber",
                table: "cars",
                column: "StateNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cars_StatusId",
                table: "cars",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_VinNumber",
                table: "cars",
                column: "VinNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clients_Email",
                table: "clients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clients_PhoneNumber",
                table: "clients",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clients_UserId",
                table: "clients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_expenses_ExpenseTypeId",
                table: "expenses",
                column: "ExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_expenses_PartSetId",
                table: "expenses",
                column: "PartSetId");

            migrationBuilder.CreateIndex(
                name: "IX_expenses_TaxId",
                table: "expenses",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_guarantees_OrderId",
                table: "guarantees",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_CarId",
                table: "notifications",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_ClientId",
                table: "notifications",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_StatusId",
                table: "notifications",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_TypeId",
                table: "notifications",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_part_sets_OrderId",
                table: "part_sets",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_part_sets_PositionId",
                table: "part_sets",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_part_sets_ProposalId",
                table: "part_sets",
                column: "ProposalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_parts_CategoryId",
                table: "parts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_journal_BillId",
                table: "payment_journal",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_journal_MethodId",
                table: "payment_journal",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_positions_CellId",
                table: "positions",
                column: "CellId");

            migrationBuilder.CreateIndex(
                name: "IX_positions_PartId",
                table: "positions",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_schedules_ShiftId",
                table: "schedules",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_schedules_WorkerId",
                table: "schedules",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_skills_SpecializationId",
                table: "skills",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_skills_WorkerId",
                table: "skills",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_supplies_SupplierId",
                table: "supplies",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_supply_sets_PositionId",
                table: "supply_sets",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_supply_sets_SupplyId",
                table: "supply_sets",
                column: "SupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_taxes_TypeId",
                table: "taxes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Login",
                table: "users",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_work_orders_CarId",
                table: "work_orders",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_work_orders_PriorityId",
                table: "work_orders",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_work_orders_StatusId",
                table: "work_orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_workers_Email",
                table: "workers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_workers_PhoneNumber",
                table: "workers",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_workers_UserId",
                table: "workers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_works_in_order_JobId",
                table: "works_in_order",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_works_in_order_OrderId",
                table: "works_in_order",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_works_in_order_StatusId",
                table: "works_in_order",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_works_in_order_WorkerId",
                table: "works_in_order",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_works_in_order_WorkProposalId",
                table: "works_in_order",
                column: "WorkProposalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "absences");

            migrationBuilder.DropTable(
                name: "acceptance_imgs");

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
                name: "absence_types");

            migrationBuilder.DropTable(
                name: "acceptances");

            migrationBuilder.DropTable(
                name: "attachments");

            migrationBuilder.DropTable(
                name: "expense_types");

            migrationBuilder.DropTable(
                name: "part_sets");

            migrationBuilder.DropTable(
                name: "taxes");

            migrationBuilder.DropTable(
                name: "notification_statuses");

            migrationBuilder.DropTable(
                name: "notification_types");

            migrationBuilder.DropTable(
                name: "bills");

            migrationBuilder.DropTable(
                name: "payment_methods");

            migrationBuilder.DropTable(
                name: "shifts");

            migrationBuilder.DropTable(
                name: "specializations");

            migrationBuilder.DropTable(
                name: "supplies");

            migrationBuilder.DropTable(
                name: "work_in_order_statuses");

            migrationBuilder.DropTable(
                name: "additional_work_proposals");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.DropTable(
                name: "tax_types");

            migrationBuilder.DropTable(
                name: "bill_statuses");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "work_orders");

            migrationBuilder.DropTable(
                name: "work_proposal_statuses");

            migrationBuilder.DropTable(
                name: "workers");

            migrationBuilder.DropTable(
                name: "works_catalog");

            migrationBuilder.DropTable(
                name: "parts");

            migrationBuilder.DropTable(
                name: "storage_cells");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "order_priorities");

            migrationBuilder.DropTable(
                name: "order_statuses");

            migrationBuilder.DropTable(
                name: "part_categories");

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
