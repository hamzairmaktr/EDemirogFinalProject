using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfcategoryCorrect(product.CategoryId),
                CheckIfCategoryLimit(product.CategoryId));

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }
        
        public IDataResult<List<Product>> GetAll()
        {
            return new SucessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
        }
        
        public IDataResult<Product> GetById(int id)
        {
            return new SucessDataResult<Product>(_productDal.Get(p=>p.ProductId==id),"Mesaj");
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SucessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(),"mesaj");
        }

        public IResult Update(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfcategoryCorrect(product.CategoryId),
                CheckIfCategoryLimit(product.CategoryId));

            if (result != null)
            {
                return result;
            }

            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        private IResult CheckIfProductCountOfcategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult("Ürünün kategorisi max limitine ulaştı");
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string name)
        {
            var result = _productDal.GetAll(p => p.ProductName == name).Any();
            if (result) 
            {
                return new ErrorResult(Messages.UrunNameMevcutError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimit(int categoyId)
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
