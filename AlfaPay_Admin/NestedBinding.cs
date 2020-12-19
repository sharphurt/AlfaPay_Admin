using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace AlfaPay_Admin
{
    [ContentProperty(nameof(Bindings))]
    public class NestedBinding : MarkupExtension
    {
        public NestedBinding()
        {
            Bindings = new Collection<BindingBase>();
        }

        public Collection<BindingBase> Bindings { get; }

        public IMultiValueConverter Converter { get; set; }

        public object ConverterParameter { get; set; }

        public CultureInfo ConverterCulture { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (!Bindings.Any())
                throw new ArgumentNullException(nameof(Bindings));
            if (Converter == null)
                throw new ArgumentNullException(nameof(Converter));

            var target = (IProvideValueTarget) serviceProvider.GetService(typeof(IProvideValueTarget));
            if (target.TargetObject is Collection<BindingBase>)
            {
                var binding = new Binding
                {
                    Source = this
                };
                return binding;
            }

            var multiBinding = new MultiBinding
            {
                Mode = BindingMode.OneWay
            };
            var tree = GetNestedBindingsTree(this, multiBinding);
            var converter = new NestedBindingConverter(tree);
            multiBinding.Converter = converter;

            return multiBinding.ProvideValue(serviceProvider);
        }

        private static NestedBindingsTree GetNestedBindingsTree(NestedBinding nestedBinding, MultiBinding multiBinding)
        {
            var tree = new NestedBindingsTree
            {
                Converter = nestedBinding.Converter,
                ConverterParameter = nestedBinding.ConverterParameter,
                ConverterCulture = nestedBinding.ConverterCulture
            };
            foreach (var bindingBase in nestedBinding.Bindings)
            {
                var binding = bindingBase as Binding;
                var childNestedBinding = binding?.Source as NestedBinding;
                if (childNestedBinding != null && binding.Converter == null)
                {
                    tree.Nodes.Add(GetNestedBindingsTree(childNestedBinding, multiBinding));
                    continue;
                }

                tree.Nodes.Add(new NestedBindingNode(multiBinding.Bindings.Count));
                multiBinding.Bindings.Add(bindingBase);
            }

            return tree;
        }
    }
    
    public class NestedBindingNode
    {
        public NestedBindingNode(int index)
        {
            Index = index;
        }

        public int Index { get; }

        public override string ToString()
        {
            return Index.ToString();
        }
    }

    public class NestedBindingsTree : NestedBindingNode
    {
        public NestedBindingsTree() : base(-1)
        {
            Nodes = new List<NestedBindingNode>();
        }

        public IMultiValueConverter Converter { get; set; }

        public object ConverterParameter { get; set; }

        public CultureInfo ConverterCulture { get; set; }

        public List<NestedBindingNode> Nodes { get; private set; }
    }
}