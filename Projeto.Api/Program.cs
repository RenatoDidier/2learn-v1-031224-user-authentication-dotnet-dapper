using Projeto.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AdicionarConfiguracaoSegredos();
builder.AdicionarConfiguracaoSendgrid();
builder.AdicionarConfiguracaoCors();
//Adicionando Autentica��o
builder.AdicionarAutenticacao();
// Configura��o das Policies
builder.AdicionarAutorizacao();

// Inje��o de depend�ncia
builder.Services.AdicionarBanco();
builder.Services.AdicionarUsuarioContext();
builder.Services.AdicionarAutenticacaoContext();

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AdicionarMediator();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
