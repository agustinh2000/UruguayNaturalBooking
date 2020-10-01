using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CategoryModel : ModelBase<Category, CategoryModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CategoryModel() { }

        public CategoryModel(Category category)
        {
            SetModel(category);
        }

        public override Category ToEntity() => new Category()
        {
            Id = Id, 
            Name = Name
        };

        protected override CategoryModel SetModel(Category category)
        {
            Id = category.Id; 
            Name = category.Name;
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CategoryModel categoryModel = (CategoryModel)obj;
                return Name.Equals(categoryModel.Name);
            }
        }
    }
}
