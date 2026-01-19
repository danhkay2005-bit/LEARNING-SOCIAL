-- ==========================================
-- SCRIPT KI?M TRA DATABASE CHO PH?N BÌNH LU?N
-- ==========================================

USE [LEARNING-SOCIAL]; -- Thay tên database c?a b?n
GO

PRINT '========================================';
PRINT 'B??C 1: KI?M TRA C?U TRÚC B?NG';
PRINT '========================================';

-- Ki?m tra b?ng BinhLuanBaiDang
IF OBJECT_ID('BinhLuanBaiDang', 'U') IS NOT NULL
BEGIN
    PRINT '? B?ng BinhLuanBaiDang t?n t?i';
    SELECT TOP 5 MaBinhLuan, MaBaiDang, NoiDung, MaNguoiDung, ThoiGianTao 
    FROM BinhLuanBaiDang 
    WHERE DaXoa != 1
    ORDER BY ThoiGianTao DESC;
END
ELSE
    PRINT '? B?ng BinhLuanBaiDang KHÔNG t?n t?i!';

PRINT '';

-- Ki?m tra b?ng ReactionBinhLuan
IF OBJECT_ID('ReactionBinhLuan', 'U') IS NOT NULL
BEGIN
    PRINT '? B?ng ReactionBinhLuan t?n t?i';
    SELECT TOP 5 MaBinhLuan, MaNguoiDung, LoaiReaction, ThoiGian 
    FROM ReactionBinhLuan 
    ORDER BY ThoiGian DESC;
END
ELSE
    PRINT '? B?ng ReactionBinhLuan KHÔNG t?n t?i!';

PRINT '';
PRINT '========================================';
PRINT 'B??C 2: KI?M TRA PRIMARY KEY';
PRINT '========================================';

-- Check Primary Key c?a ReactionBinhLuan
SELECT 
    tc.TABLE_NAME,
    kcu.COLUMN_NAME,
    tc.CONSTRAINT_NAME
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu 
    ON tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
WHERE tc.TABLE_NAME = 'ReactionBinhLuan' 
  AND tc.CONSTRAINT_TYPE = 'PRIMARY KEY';

PRINT '';
PRINT '========================================';
PRINT 'B??C 3: KI?M TRA FOREIGN KEY CONSTRAINTS';
PRINT '========================================';

-- Check Foreign Key c?a ReactionBinhLuan
SELECT 
    fk.name AS ForeignKey_Name,
    OBJECT_NAME(fk.parent_object_id) AS Table_Name,
    COL_NAME(fkc.parent_object_id, fkc.parent_column_id) AS Column_Name,
    OBJECT_NAME(fk.referenced_object_id) AS Referenced_Table,
    COL_NAME(fkc.referenced_object_id, fkc.referenced_column_id) AS Referenced_Column
FROM sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
WHERE OBJECT_NAME(fk.parent_object_id) = 'ReactionBinhLuan';

PRINT '';
PRINT '========================================';
PRINT 'B??C 4: KI?M TRA D? LI?U M?U';
PRINT '========================================';

-- ??m s? bình lu?n
DECLARE @CommentCount INT;
SELECT @CommentCount = COUNT(*) FROM BinhLuanBaiDang WHERE DaXoa != 1;
PRINT 'T?ng s? bình lu?n: ' + CAST(@CommentCount AS VARCHAR);

-- ??m s? reaction
DECLARE @ReactionCount INT;
SELECT @ReactionCount = COUNT(*) FROM ReactionBinhLuan;
PRINT 'T?ng s? reaction: ' + CAST(@ReactionCount AS VARCHAR);

-- Th?ng kê reaction theo lo?i
PRINT '';
PRINT 'Th?ng kê reaction theo lo?i:';
SELECT 
    LoaiReaction,
    COUNT(*) AS SoLuong
FROM ReactionBinhLuan
GROUP BY LoaiReaction
ORDER BY SoLuong DESC;

PRINT '';
PRINT '========================================';
PRINT 'B??C 5: KI?M TRA ORPHAN RECORDS';
PRINT '========================================';

-- Tìm reactions không có bình lu?n t??ng ?ng
DECLARE @OrphanCount INT;
SELECT @OrphanCount = COUNT(*)
FROM ReactionBinhLuan r
LEFT JOIN BinhLuanBaiDang b ON r.MaBinhLuan = b.MaBinhLuan
WHERE b.MaBinhLuan IS NULL;

IF @OrphanCount > 0
BEGIN
    PRINT '?? Có ' + CAST(@OrphanCount AS VARCHAR) + ' reaction không có bình lu?n t??ng ?ng!';
    SELECT r.* 
    FROM ReactionBinhLuan r
    LEFT JOIN BinhLuanBaiDang b ON r.MaBinhLuan = b.MaBinhLuan
    WHERE b.MaBinhLuan IS NULL;
END
ELSE
    PRINT '? Không có orphan records';

PRINT '';
PRINT '========================================';
PRINT 'B??C 6: TEST INSERT/DELETE REACTION';
PRINT '========================================';

-- L?y m?t bình lu?n m?u
DECLARE @TestCommentId INT;
DECLARE @TestUserId UNIQUEIDENTIFIER;

SELECT TOP 1 
    @TestCommentId = MaBinhLuan,
    @TestUserId = MaNguoiDung
FROM BinhLuanBaiDang 
WHERE DaXoa != 1;

IF @TestCommentId IS NOT NULL
BEGIN
    PRINT 'Test v?i MaBinhLuan = ' + CAST(@TestCommentId AS VARCHAR);
    PRINT 'Test v?i MaNguoiDung = ' + CAST(@TestUserId AS VARCHAR(50));
    
    -- Test INSERT
    BEGIN TRY
        INSERT INTO ReactionBinhLuan (MaBinhLuan, MaNguoiDung, LoaiReaction, ThoiGian)
        VALUES (@TestCommentId, @TestUserId, N'Thich', GETDATE());
        PRINT '? Test INSERT thành công';
        
        -- Test DELETE
        DELETE FROM ReactionBinhLuan 
        WHERE MaBinhLuan = @TestCommentId AND MaNguoiDung = @TestUserId;
        PRINT '? Test DELETE thành công';
    END TRY
    BEGIN CATCH
        PRINT '? Test th?t b?i: ' + ERROR_MESSAGE();
    END CATCH
END
ELSE
BEGIN
    PRINT '?? Không có d? li?u bình lu?n ?? test';
END

PRINT '';
PRINT '========================================';
PRINT 'B??C 7: KI?M TRA INDEX';
PRINT '========================================';

-- Li?t kê indexes trên ReactionBinhLuan
SELECT 
    i.name AS IndexName,
    i.type_desc AS IndexType,
    COL_NAME(ic.object_id, ic.column_id) AS ColumnName
FROM sys.indexes i
JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
WHERE OBJECT_NAME(i.object_id) = 'ReactionBinhLuan'
ORDER BY i.name, ic.key_ordinal;

PRINT '';
PRINT '========================================';
PRINT 'K?T QU?: KI?M TRA HOÀN T?T';
PRINT '========================================';
PRINT 'N?u t?t c? b??c ??u ? thì database ?ã s?n sàng!';
PRINT 'N?u có ? hãy xem l?i c?u trúc database ho?c ch?y migrations.';
GO
