using ApiAula.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAula.Controllers
{
    [ApiController]
    [Route("teste/aula")]
    public class ProdutoControler : ControllerBase
    {
        private readonly ApplicationDbContext _context; //padrão de código de injeção de dependência

        public ProdutoControler(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Produto produto) //retorna um inteiro em paralelo, executando uma tarefa à parte
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto.Id;
        }
        [HttpGet]
        public async Task<ActionResult<List<Produto>>> GetProdutos()
        {
            return await _context.Produtos.ToListAsync(); //await manda o framework esperar todos os resultados, fazendo com que tudo aconteça de maneira síncrona
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Produto>> GetProdutos(int id)
        {
            Produto produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
            if(produto == null)
            {
                return NotFound();
            }
            return produto;
        }
    }
}
