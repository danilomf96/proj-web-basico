using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using System.ComponentModel.DataAnnotations;

[Route("api/[controller]")]
[ApiController]
public class PessoasController : ControllerBase
{
    private readonly AppDataContext _context;

    public PessoasController(AppDataContext context)
    {
        _context = context;
    }

    // GET: api/pessoas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoas()
    {
        return await _context.Pessoas.ToListAsync();
    }

    // GET: api/pessoas/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Pessoa>> GetPessoa(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa == null)
        {
            return NotFound();
        }
        return pessoa;
    }

    // POST: api/pessoas/adicionar
    [HttpPost("adicionar")]
    public async Task<ActionResult<Pessoa>> PostPessoa(Pessoa pessoa)
    {
        // Validação dos atributos da pessoa
        List<ValidationResult> erros = new();
        if (!Validator.TryValidateObject(pessoa, new ValidationContext(pessoa), erros, true))
        {
            return BadRequest(erros);
        }

        // Adiciona a pessoa ao banco de dados
        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();

        // Retorna a resposta criada com o novo recurso
        return CreatedAtAction("GetPessoa", new { id = pessoa.Id }, pessoa);
    }

    // PUT: api/pessoas/alterar/{id}
    [HttpPut("alterar/{id}")]
    public async Task<IActionResult> PutPessoa(int id, Pessoa pessoa)
    {
        if (id != pessoa.Id)
        {
            return BadRequest();
        }

        _context.Entry(pessoa).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PessoaExists(id))
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

    // DELETE: api/pessoas/deletar/{id}
    [HttpDelete("deletar/{id}")]
    public async Task<IActionResult> DeletePessoa(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa == null)
        {
            return NotFound();
        }

        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool PessoaExists(int id)
    {
        return _context.Pessoas.Any(e => e.Id == id);
    }
}
