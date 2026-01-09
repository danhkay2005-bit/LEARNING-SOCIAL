SELECT * FROM CapDo;

INSERT INTO CapDo (TenCapDo, MucXPToiThieu, MucXPToiDa)
VALUES (N'Level 1', 0, 100);
SELECT TOP 10 MaCapDo, TenCapDo FROM CapDo ORDER BY MaCapDo;
SELECT * FROM NguoiDung;