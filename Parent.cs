namespace BlazorWasmEfCoreNormalized;

public class Parent
{
    public int Id { get; set; }
    public string ParentValue { get; set; }
    public List<Child> Children { get; set; }
}