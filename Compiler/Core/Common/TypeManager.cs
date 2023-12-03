namespace Compiler.Core.Common;

public static class TypeManager
{
    public enum DataType
    {
        Integer,
        Float64,
        String,
        Bool
    }
    
    public static DataType GetDataType(string type)
    {
        return type switch
        {
            "Integer" => DataType.Integer,
            "Float64" => DataType.Float64,
            "String" => DataType.String,
            "Bool" => DataType.Bool,
            _ => throw new ArgumentException($"Unknown type: {type}")
        };
    }

    public static DataType? GetCommonType(DataType leftType, DataType rightType)
    {
        return (leftType, rightType) switch
        {
            (DataType.Integer, DataType.Integer) => DataType.Integer,
            (DataType.Float64, DataType.Float64) => DataType.Float64,
            (DataType.String, DataType.String) => DataType.String,
            _ => null
        };
    }
}