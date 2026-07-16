using API_AgendaDeContatos.Data;
using API_AgendaDeContatos.DTOs;
using API_AgendaDeContatos.Enums;
using API_AgendaDeContatos.Models;
using API_AgendaDeContatos.Repositories;
using API_AgendaDeContatos.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Praticando MinimalAPI

// GET - Buscar todos
app.MapGet("/contatos", async (AppDbContext db) =>
    await db.Contatos.ToListAsync());

// POST - Adicionar
app.MapPost("/novoContato", async (AppDbContext db, Contato contato) => {
    db.Contatos.Add(contato);
    await db.SaveChangesAsync();
    return Results.Created($"/novoContato/{contato.Id}", contato);
});

// PUT - Atualizar
app.MapPut("/atualizar{id}", async (AppDbContext db, AtualizaContatoDto contatoDto, int id) => {
    var contatoASertualizado = await db.Contatos.FindAsync(id);
    if (contatoASertualizado is null) return Results.NotFound();

    contatoASertualizado.Nome = contatoDto.Nome;
    contatoASertualizado.Telefone = contatoDto.Telefone;
    contatoASertualizado.Email = contatoDto.Email;
    contatoASertualizado.Favorito = contatoDto.Favorito;
    contatoASertualizado.Categoria = contatoDto.Categoria;
    contatoASertualizado.Sobrenome = contatoDto.Sobrenome;

    await db.SaveChangesAsync();
    return Results.Ok("Contato atualizado");
});

// DELETE - Deletar
app.MapDelete("/deletar", async (AppDbContext db, int id) => {
    var contato = await db.Contatos.FindAsync(id);
    if (contato is null) return Results.NotFound();

    db.Contatos.Remove(contato);
    await db.SaveChangesAsync();
    return Results.Ok("Contato deletado");
});

app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();
