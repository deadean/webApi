﻿CREATE TABLE `Tasks`
(
       Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
       Subject TEXT,
       StartDate DATETIME,
       DueDate DATETIME,
       CompletedDate DATETIME,
       StatusId INTEGER,
       CreatedDate DATETIME,
       CreatedUserId INTEGER,
       ts DATETIME,
       FOREIGN KEY (StatusId) REFERENCES Status(Id),
       FOREIGN KEY (CreatedUserId) REFERENCES Users(Id)
)