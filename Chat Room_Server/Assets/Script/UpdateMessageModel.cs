public class UpdateMessageModel : BaseModel
{
    public static readonly string CLASS_NAME = typeof(UpdateMessageModel).Name;
    public UpdateMessageModel() : base(CLASS_NAME) { }
    public string messageData { get; set; }
}