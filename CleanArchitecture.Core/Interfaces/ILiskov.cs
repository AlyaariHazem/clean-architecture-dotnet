using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces
{
    public interface ILiskov
    {
    }


    //public class overloading
    //{
    //    public string Build(string firstName, string lastName)
    //    {
    //        return $"{firstName} {lastName}";
    //    }

    //    public string Build(string firstName, string lastName, int age)
    //    {
    //        return $"{firstName} {lastName}  {age}";
    //    }
    //}

    //public class ParentOverriding
    //{
    //    public virtual string build(string firstName, string lastName)
    //    {
    //        return string.Empty;
    //    }
    //}

    //public class ChildOverriding : ParentOverriding
    //{
    //    public override string build(string firstName, string lastName)
    //    {
    //        return base.build(firstName, lastName);
    //    }
    //}

}
