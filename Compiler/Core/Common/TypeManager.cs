namespace Compiler.Core.Common;

public static class TypeManager
{
    public enum DataType
    {
        Void, // Only used for return types
        Any, // Only used for built-in functions
        Integer,
        Float64,
        String,
        Bool
    }
    
    public static DataType? GetDataType(string type)
    {
        return type switch
        {
            "Integer" => DataType.Integer,
            "Float64" => DataType.Float64,
            "String" => DataType.String,
            "Bool" => DataType.Bool,
            _ => null
        };
    }
    
    public static bool IsNumeric(DataType? type)
    {
        return type switch
        {
            DataType.Integer => true,
            DataType.Float64 => true,
            _ => false
        };
    }
    
    public static bool IsBoolean(DataType? type)
    {
        return type == DataType.Bool;
    }
    
    public static DataType? GetCommonType(DataType leftType, DataType rightType)
    {
        return (leftType, rightType) switch
        {
            (DataType.Integer, DataType.Integer) => DataType.Integer,
            (DataType.Float64, DataType.Float64) => DataType.Float64,
            (DataType.String, DataType.String) => DataType.String,
            (DataType.Bool, DataType.Bool) => DataType.Bool,
            _ => null
        };
    }
    
    public static DataType? GetCommonNumericType(DataType leftType, DataType rightType)
    {
        DataType? type = GetCommonType(leftType, rightType);
        return !IsNumeric(type) ? null : type;
    }
}