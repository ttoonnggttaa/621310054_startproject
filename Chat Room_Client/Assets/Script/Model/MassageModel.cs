public class MessageModel : BaseModel
{
    public static readonly string CLASS_NAME = typeof(MessageModel).Name;
    public MessageModel() : base(CLASS_NAME) { }
    public string sender { get; set; }
    public string message { get; set; }
}