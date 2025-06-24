using ApiDeProdutos.Models;

namespace ApiDeProdutos.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetProdutosAsync();

        Task<Produto?> GetProdutoByIdAsync(int id);

        Task<IEnumerable<Produto>> GetProdutosByNomeAsync(string nome);

        Task CreateProdutoAsync(Produto produto);

        Task UpdateProdutoAsync(Produto produto);

        Task DeleteProdutoAsync(Produto produto);
    }
}
