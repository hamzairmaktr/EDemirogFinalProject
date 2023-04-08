using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
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

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
           

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
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
