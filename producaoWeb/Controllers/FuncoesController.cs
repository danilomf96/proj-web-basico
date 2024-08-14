using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using System.ComponentModel.DataAnnotations;

[Route("api/[controller]")]
[ApiController]
public class FuncoesController : ControllerBase
{
    private readonly AppDataContext _context;

    public FuncoesController(AppDataContext context)
    {
        _context = context;
    }

    // GET: api/funcoes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Funcao>>> GetFuncoes()
    {
        return await _context.Funcoes.ToListAsync();
    }

    // GET: api/funcoes/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Funcao>> GetFuncao(string id)
    {
        var funcao = await _context.Funcoes.FindAsync(id);
        if (funcao == null)
        {
            return NotFound();
        }
        return funcao;
    }

    // POST: api/funcoes/adicionar
    [HttpPost("adicionar")]
    public async Task<ActionResult<Funcao>> PostFuncao(Funcao funcao)
    {
        // Validação dos atributos da função
        List<ValidationResult> erros = new();
        if (!Validator.TryValidateObject(funcao, new ValidationContext(funcao), erros, true))
        {
            return BadRequest(erros);
        }

        // Adiciona a função ao banco de dados
        _context.Funcoes.Add(funcao);
        await _context.SaveChangesAsync();

        // Retorna a resposta criada com o novo recurso
        return CreatedAtAction("GetFuncao", new { id = funcao.Id }, funcao);
    }

    // PUT: api/funcoes/alterar/{id}
    [HttpPut("alterar/{id}")]
    public async Task<IActionResult> PutFuncao(string id, Funcao funcao)
    {
        if (id != funcao.Id)
        {
            return BadRequest();
        }

        _context.Entry(funcao).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FuncaoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/funcoes/deletar/{id}
    [HttpDelete("deletar/{id}")]
    public async Task<IActionResult> DeleteFuncao(string id)
    {
        var funcao = await _context.Funcoes.FindAsync(id);
        if (funcao == null)
        {
            return NotFound();
        }

        _context.Funcoes.Remove(funcao);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool FuncaoExists(string id)
    {
        return _context.Funcoes.Any(e => e.Id == id);
    }
}
