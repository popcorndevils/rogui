
namespace Rogui
{
    public abstract class BaseBox : Aspect
    {
        public BaseBox() : base() {}
        public BaseBox(params Aspect[] aspects) : base(aspects) {}

        public override void Add(params Aspect[] aspects)
        {
            base.Add(aspects);
            if(this.Parent is not null)
            {
                foreach(Aspect a in aspects)
                {
                    a.Parent = this.Parent;
                }
            }
        }

        public override void Insert(int index, Aspect aspect)
        {
            base.Insert(index, aspect);
            if(this.Parent is not null)
            {
                aspect.Parent = this.Parent;
            }
        }
    }
}