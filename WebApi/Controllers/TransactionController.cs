using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _financialTransactionService;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ITransactionService financialTransactionService, IMapper mapper, ILogger<TransactionController> logger)
        {
            _financialTransactionService = financialTransactionService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Fetches transactions history
        /// </summary>
        /// <returns></returns>
        /// <response code="200">successful operation</response>
        /// <response code="400">invalid status value</response>           
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
        {
            try
            {
                var transactions = await _financialTransactionService.GetTransactions();
                var result = _mapper.Map<IEnumerable<TransactionDTO>>(transactions);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Find transaction by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a single transaction object</returns>        
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(Guid id)
        {
            try
            {
                var financialTransaction = await _financialTransactionService.GetTransaction(id);
                var result = _mapper.Map<TransactionDTO>(financialTransaction);
                if (financialTransaction == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Commit new transaction to the account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>           
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [HttpPost]
        public async Task<ActionResult<Transaction>> CommitTransaction(SaveTransactionRequest request)
        {
            try
            {
                var financialTransaction = new Transaction()
                {
                    Type = request.Type,
                    Amount = request.Amount,
                    EffectiveDate = DateTime.UtcNow
                };
                if (!_financialTransactionService.IsValidTransaction(financialTransaction).Result)
                {
                    return StatusCode(406);
                }
                await _financialTransactionService.SaveTransaction(financialTransaction);

                return new JsonResult(financialTransaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Fetches current account balance
        /// </summary>        
        /// <returns></returns>
        /// <response code="200">successful operation</response>
        [HttpGet]
        [Route("account-balance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<decimal>> GetAccountBalance()
        {
            try
            {
                return await _financialTransactionService.GetAccountBalance();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}
