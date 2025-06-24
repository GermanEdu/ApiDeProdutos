using ApiDeProdutos.Models;
using ApiDeProdutos.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDeProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProdutosController : ControllerBase
    {
        private IProdutoService _produtoService;

        public ProdutosController(IProdutoService ProdutoService)
        {
            _produtoService = ProdutoService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Produto>>> GetProdutos()
        {
            try
            {
                var Produtos = await _produtoService.GetProdutosAsync();
                return Ok(Produtos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter os Produtos");
            }
        }


        [HttpGet("ProdutosPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Produto>>>
            GetProdutosByName([FromQuery] string nome)
        {


            try
            {
                
                var Produtos = await _produtoService.GetProdutosByNomeAsync(nome);

                if (Produtos.Count() == 0)
                    return NotFound($"Não existe Produtos com {nome}");

                return Ok(Produtos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter os Produtos");
            }
        }



        //retornar pelo Id 


        [HttpGet("{id:int}", Name ="GetProduto")]
        public async Task<ActionResult<Produto>> GetProduto(int id)

        { 
            try
            {
                var Produto = await _produtoService.GetProdutoByIdAsync(id);

                return Ok(Produto);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter o Produto com id={ id}");
            }
        }




        //Criar Recurso método Post 

        [HttpPost]
        public async Task<ActionResult> Create(Produto Produto)
        {
            try
            {
                await _produtoService.CreateProdutoAsync(Produto);

                return CreatedAtRoute(nameof(GetProduto), new {id = Produto.Id}, Produto);

            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }



        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] Produto Produto)
        {
            try
            {
                if(Produto.Id == id)
                {
                await _produtoService.UpdateProdutoAsync(Produto);
                return NoContent();

                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }

            }
            catch
            {
                return BadRequest("Erro ao Atualizar");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
               
                  var Produto =  await _produtoService.GetProdutoByIdAsync(id);
                  
                if(Produto != null)
                {
                    await _produtoService.DeleteProdutoAsync(Produto);
                    return Ok($"Produto de id={id} foi excluido com sucesso !!");
                }
                else
                {
                    return NotFound("Id não existe");
                }

            }
            catch
            {
                return BadRequest("Erro ao Atualizar");
            }
        }


    }
}
