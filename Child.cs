namespace BlazorWasmEfCoreNormalized;

public class Child
{
    public int Id { get; set; }
    public string ChildValue { get; set; }

    public Parent Parent { get; set; }
    public int ParentId { get; set; }
}