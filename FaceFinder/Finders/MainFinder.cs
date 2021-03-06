﻿using System.Linq;
using System.Reflection;
using FaceFinder.Data;

namespace FaceFinder.Finders
{
    public class MainFinder : AbstractFinder<InputData, object>
    {
        public MainFinder()
        {
            Children = new AbstractFinder[]
            {
                new VkFinder()
            };
        }
        public override string Name { get; } = "Вся информация";
        public override void Find()
        {
            GetF<VkFinder>().Input = Input;
            base.Find();
            if (Finders.Count(x => x.IsReliable != true) > 0)
            {
                Output = Finders.Where(x => x.IsReliable != true)
                    .ToDictionary(x=> x.Name, x => x.IsReliable.HasValue ? x.GetType().GetRuntimeProperty("Output").GetValue(x) : null);
            }
            else
            {
                Output = true;
            }
        }
    }
}
