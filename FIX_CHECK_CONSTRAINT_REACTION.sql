-- ==========================================
-- SCRIPT KI?M TRA VÀ S?A L?I CHECK CONSTRAINT
-- ==========================================

USE [app_MXH];  -- Thay tên database c?a b?n
GO

PRINT '========================================';
PRINT 'B??C 1: KI?M TRA CHECK CONSTRAINTS';
PRINT '========================================';

-- Li?t kê t?t c? CHECK constraints trên ReactionBinhLuan
SELECT 
    cc.name AS ConstraintName,
    OBJECT_NAME(cc.parent_object_id) AS TableName,
    COL_NAME(cc.parent_object_id, cc.parent_column_id) AS ColumnName,
    cc.definition AS CheckDefinition
FROM sys.check_constraints cc
WHERE OBJECT_NAME(cc.parent_object_id) = 'ReactionBinhLuan';

PRINT '';
PRINT '========================================';
PRINT 'B??C 2: KI?M TRA GIÁ TR? HI?N CÓ';
PRINT '========================================';

-- Xem các giá tr? LoaiReaction ?ang có trong database
SELECT DISTINCT 
    LoaiReaction,
    COUNT(*) AS SoLuong
FROM ReactionBinhLuan
GROUP BY LoaiReaction;

PRINT '';
PRINT '========================================';
PRINT 'B??C 3: PHÂN TÍCH V?N ??';
PRINT '========================================';

-- Check xem column type
SELECT 
    c.name AS ColumnName,
    t.name AS DataType,
    c.max_length AS MaxLength,
    c.is_nullable AS IsNullable,
    c.collation_name AS Collation
FROM sys.columns c
JOIN sys.types t ON c.user_type_id = t.user_type_id
WHERE c.object_id = OBJECT_ID('ReactionBinhLuan')
  AND c.name = 'LoaiReaction';

PRINT '';
PRINT '========================================';
PRINT 'GI?I PHÁP: DROP CHECK CONSTRAINT C?';
PRINT '========================================';

-- L?y tên constraint ??ng
DECLARE @ConstraintName NVARCHAR(128);
SELECT @ConstraintName = cc.name
FROM sys.check_constraints cc
WHERE OBJECT_NAME(cc.parent_object_id) = 'ReactionBinhLuan'
  AND COL_NAME(cc.parent_object_id, cc.parent_column_id) = 'LoaiReaction';

IF @ConstraintName IS NOT NULL
BEGIN
    DECLARE @SQL NVARCHAR(500) = 'ALTER TABLE ReactionBinhLuan DROP CONSTRAINT ' + QUOTENAME(@ConstraintName);
    PRINT '?ang xóa constraint: ' + @ConstraintName;
    EXEC sp_executesql @SQL;
    PRINT '? ?ã xóa CHECK constraint c?';
END
ELSE
BEGIN
    PRINT '?? Không tìm th?y CHECK constraint';
END

PRINT '';
PRINT '========================================';
PRINT 'GI?I PHÁP: T?O CHECK CONSTRAINT M?I';
PRINT '========================================';

-- T?o constraint m?i v?i giá tr? ?úng (d?a trên enum)
ALTER TABLE ReactionBinhLuan
ADD CONSTRAINT CK_ReactionBinhLuan_LoaiReaction
CHECK (LoaiReaction IN (N'Thich', N'YeuThich', N'Haha', N'Wow', N'Buon', N'TucGian'));

PRINT '? ?ã t?o CHECK constraint m?i v?i các giá tr?:';
PRINT '   - Thich (1)';
PRINT '   - YeuThich (2)';
PRINT '   - Haha (3)';
PRINT '   - Wow (4)';
PRINT '   - Buon (5)';
PRINT '   - TucGian (6)';

PRINT '';
PRINT '========================================';
PRINT 'B??C 4: KI?M TRA SAU KHI S?A';
PRINT '========================================';

-- Ki?m tra constraint m?i
SELECT 
    cc.name AS ConstraintName,
    cc.definition AS CheckDefinition
FROM sys.check_constraints cc
WHERE OBJECT_NAME(cc.parent_object_id) = 'ReactionBinhLuan'
  AND COL_NAME(cc.parent_object_id, cc.parent_column_id) = 'LoaiReaction';

PRINT '';
PRINT '========================================';
PRINT 'B??C 5: TEST INSERT';
PRINT '========================================';

-- Test insert v?i giá tr? h?p l?
BEGIN TRY
    -- L?y m?t comment test
    DECLARE @TestCommentId INT;
    DECLARE @TestUserId UNIQUEIDENTIFIER;
    
    SELECT TOP 1 
        @TestCommentId = MaBinhLuan,
        @TestUserId = MaNguoiDung
    FROM BinhLuanBaiDang 
    WHERE DaXoa != 1;
    
    IF @TestCommentId IS NOT NULL
    BEGIN
        -- Test INSERT v?i giá tr? enum ?úng
        INSERT INTO ReactionBinhLuan (MaBinhLuan, MaNguoiDung, LoaiReaction, ThoiGian)
        VALUES (@TestCommentId, @TestUserId, N'Thich', GETDATE());
        
        PRINT '? Test INSERT thành công v?i LoaiReaction = N''Thich''';
        
        -- Cleanup
        DELETE FROM ReactionBinhLuan 
        WHERE MaBinhLuan = @TestCommentId AND MaNguoiDung = @TestUserId;
        
        PRINT '? Test DELETE thành công';
    END
    ELSE
    BEGIN
        PRINT '?? Không có d? li?u comment ?? test';
    END
END TRY
BEGIN CATCH
    PRINT '? Test th?t b?i: ' + ERROR_MESSAGE();
END CATCH

PRINT '';
PRINT '========================================';
PRINT 'K?T QU?: KH?C PH?C HOÀN T?T';
PRINT '========================================';
PRINT '';
PRINT '?? H??NG D?N S? D?NG TRONG CODE:';
PRINT '';
PRINT 'C# Code ph?i s? d?ng:';
PRINT '   LoaiReaction = request.LoaiReaction.ToString()';
PRINT '';
PRINT 'Enum values ???c map thành:';
PRINT '   LoaiReactionEnum.Thich ? "Thich"';
PRINT '   LoaiReactionEnum.YeuThich ? "YeuThich"';
PRINT '   LoaiReactionEnum.Haha ? "Haha"';
PRINT '   LoaiReactionEnum.Wow ? "Wow"';
PRINT '   LoaiReactionEnum.Buon ? "Buon"';
PRINT '   LoaiReactionEnum.TucGian ? "TucGian"';
PRINT '';
PRINT '?? L?U Ý: Database l?u TÊN ENUM (string), không ph?i s?!';

GO
