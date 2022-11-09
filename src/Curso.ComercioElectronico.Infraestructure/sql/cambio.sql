BEGIN TRANSACTION;

DROP INDEX "IX_OrdenItem_OrdenId";

ALTER TABLE "OrdenItem" ADD "OrdenId1" TEXT NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

CREATE INDEX "IX_OrdenItem_OrdenId1" ON "OrdenItem" ("OrdenId1");

CREATE TABLE "ef_temp_OrdenItem" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_OrdenItem" PRIMARY KEY,
    "Cantidad" INTEGER NOT NULL,
    "Observaciones" TEXT NULL,
    "OrdenId" INTEGER NOT NULL,
    "OrdenId1" TEXT NOT NULL,
    "Precio" REAL NOT NULL,
    "ProductId" INTEGER NOT NULL,
    CONSTRAINT "FK_OrdenItem_Ordenes_OrdenId1" FOREIGN KEY ("OrdenId1") REFERENCES "Ordenes" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_OrdenItem_Productos_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Productos" ("Id") ON DELETE CASCADE       
);

INSERT INTO "ef_temp_OrdenItem" ("Id", "Cantidad", "Observaciones", "OrdenId", "OrdenId1", "Precio", "ProductId")
SELECT "Id", "Cantidad", "Observaciones", "OrdenId", "OrdenId1", "Precio", "ProductId"
FROM "OrdenItem";

CREATE TABLE "ef_temp_Ordenes" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Ordenes" PRIMARY KEY,
    "ClienteId" INTEGER NOT NULL,
    "Estado" INTEGER NOT NULL,
    "Fecha" TEXT NOT NULL,
    "FechaAnulacion" TEXT NULL,
    "Observaciones" TEXT NULL,
    "Total" TEXT NOT NULL,
    CONSTRAINT "FK_Ordenes_Clientes_ClienteId" FOREIGN KEY ("ClienteId") REFERENCES "Clientes" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_Ordenes" ("Id", "ClienteId", "Estado", "Fecha", "FechaAnulacion", "Observaciones", "Total")
SELECT "Id", "ClienteId", "Estado", "Fecha", "FechaAnulacion", "Observaciones", "Total"
FROM "Ordenes";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "OrdenItem";

ALTER TABLE "ef_temp_OrdenItem" RENAME TO "OrdenItem";

DROP TABLE "Ordenes";

ALTER TABLE "ef_temp_Ordenes" RENAME TO "Ordenes";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "IX_OrdenItem_OrdenId1" ON "OrdenItem" ("OrdenId1");

CREATE INDEX "IX_OrdenItem_ProductId" ON "OrdenItem" ("ProductId");

CREATE INDEX "IX_Ordenes_ClienteId" ON "Ordenes" ("ClienteId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221109185437_Orden-Guid', '6.0.10');

COMMIT;