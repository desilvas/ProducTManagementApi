
using NLog;
using ProductManagement.Domain;
using ProductManagement.Domain.DomainModels;
using ProductManagement.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;

namespace ProductManagement.Api.Controllers
{
    [RoutePrefix("product")]
    public class ProductsController : ApiController
    {
        private const string SYSTEM_ERROR = "A system error has occured and we could not perform this action.";
        private const string DATA_NOT_FOUND = "We could not find this data in our system.";
        private const string PRODUCT_UPDATED = "Product updated successfully";
        private const string PRODUCT_DELETED = "Product deleted successfully";

        private static Logger logger = LogManager.GetCurrentClassLogger();
        #region Dependancy Injection

        private IProductService _productService;

        [Dependency()]
        public IProductService ProductService
        {
            get { return _productService; }
            set { _productService = value; }
        }
        #endregion

        /// <summary>
        /// Get all the products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("products")]
        
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var productList = (await _productService.GetAll())
                            .Select(x => new ProductViewModel(x))
                            .ToList();

                return Ok(productList);
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
                return BadRequest(SYSTEM_ERROR);
            }
        }

        /// <summary>
        /// Get products by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> GetByName(string name)
        {
            try
            {
                var productList = (await _productService.GetByName(name))
                            .Select(x => new ProductViewModel(x))
                            .ToList();

                return Ok(productList);
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
                return BadRequest(SYSTEM_ERROR);
            }
        }

        /// <summary>
        /// Get products by Id(Guid)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetById(Guid id)
        {
            try
            {
                var productList = (await _productService.GetById(id))
                            .Select(x => new ProductViewModel(x))
                            .ToList();

                return Ok(productList);
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
                return BadRequest(SYSTEM_ERROR);
            }
        }

        /// <summary>
        /// Create product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route]
        public IHttpActionResult Create([FromBody] ProductViewModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var productModel = _productService.Add(product.ToDataModel());
                    return Ok(productModel);
                }
                else
                {
                    return BadRequest(SYSTEM_ERROR);
                }

            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        [Route]
        public IHttpActionResult Update(ProductViewModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var productModel = _productService.Update(product.ToDataModel());
                    if (productModel != null)
                    {
                        return Ok(productModel);
                    }
                    return NotFound();
                }
                else
                {
                    return BadRequest(SYSTEM_ERROR);
                }

            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                var reult = (_productService.Delete(id));
                if (reult)
                {
                    return Ok(PRODUCT_DELETED);
                }
                return NotFound();
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
                return BadRequest(SYSTEM_ERROR);
            }
        }

        /// <summary>
        /// Get Product Option
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productOption"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/options")]
        public IHttpActionResult CreateProductOption(Guid id, [FromBody] ProductOptionsViewModel productOption)
        {
            try
            {
                Guid productId;
                if (ModelState.IsValid && Guid.TryParse(id.ToString(), out productId))
                {                    
                    var result = _productService.AddProductOption(productOption.ToDataMode(productId));
                    if (result)
                    {
                        return Ok(productOption);
                    }
                    return NotFound();                    
                }
                else
                {
                    return BadRequest(SYSTEM_ERROR);
                }

            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Get Product Options By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/options")]
        public async Task<IHttpActionResult> GetProductOptionsById(Guid id)
        {
            try
            {
                var model = (await _productService.GetProductOptionsById(id))
                            .Select(x => new ProductOptionsViewModel(x))
                            .ToList();

                return Ok(model);
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
                return BadRequest(SYSTEM_ERROR);
            }
        }

        /// <summary>
        /// Get ProductOptions By Option Id and Prodcut Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="optionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/options/{optionId}")]
        public async Task<IHttpActionResult> GetProductOptionsByIdAndProdcut(Guid id, Guid optionId)
        {
            try
            {
                var model = (await _productService.GetProductOptionsByIdAndProductId(id, optionId))
                            .Select(x => new ProductOptionsViewModel(x))
                            .ToList();

                return Ok(model);
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
                return BadRequest(SYSTEM_ERROR);
            }
        }

        /// <summary>
        /// Delete a Product Option
        /// </summary>
        /// <param name="id"></param>
        /// <param name="optionId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}/options/{optionId}")]
        public IHttpActionResult DeleteProductOption(Guid id, Guid optionId)
        {
            try
            {
                var reult = (_productService.DeleteProductOption(id, optionId));
                if (reult)
                {
                    return Ok(PRODUCT_DELETED);
                }
                return NotFound();
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
                return BadRequest(SYSTEM_ERROR);
            }
        }
    }
}
