public class BaseModel
{
    public string ClassName { get; set; }
    protected BaseModel(string className) => ClassName = className;
}