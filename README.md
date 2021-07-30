# XUnit - Testes de Unidade

### Qual objetivo do testes de unidade?
Testes de unidade tem como objetivo testar uma única unidade do seu código e nada além fora dela.
<br/>
Sendo assim, o teste irá se basear somente dentro das ações daquele bloco de código e, caso haja dependências externas, as mesmas devem ser 'simuladas'.

---

### Nomenclaturas
#### Internas
- [Trait] : nomear os testes e organizá-los, de acordo com nome e valor
- [Fact] : indicando que aquele método pertence à uma execução de um fato para teste (apenas um)
``` c#
[Fact(DisplayName = "Remover produto")]
[Trait("Categoria", "Produto Service")]
public async Task RemoverProduto_ProdutoExistente_DeveSucesso() 
{
}
```

- [Theory] : indicando que aquele método pertence à uma execução de um teoria de testes (então, mais de um)
- [InlineData] : Implementa os parâmetros que serão necessários nas execuções daquela [Theory]. Para cada [InlineData] um teste será executado. Utilizado para simular mais de uma execução para aquele teste.
``` c#
[Theory(DisplayName = "Adicionar produto")]
[Trait("Categoria", "Produto Service")]
[InlineData("Produto 01", 199.90)]
[InlineData("Produto 02", 19.90)]
[InlineData("Produto 03", 0.90)]
public async Task AdicionarProduto_NovoProduto_DeveRetornarOProdutoAdicionado(string nome, decimal preco)
{
}
```

#### AAA - Organização
- Arrange : ponto do seu teste onde será implementado a criação dos dados necessários para a execução do mesmo
- Action : ponto onde irá ocorrer a execução da ação que seu teste está sendo responsável por analisar
- Assert: ponto onde irá ocorrer as validações dos resultados gerados pelo seu teste
``` c#
[Fact(DisplayName = "Definir produto")]
[Trait("Categoria", "Compra Produto")]
public void DefinirProduto_NovoProduto_DeveAlterarOProduto()
{
    // Arrange
    var produto = new Produto().DefinirNome("Produto 01").DefinirPreco(250);

    // Act
    _compraProduto.DefinirProduto(produto);

    // Assert
    _compraProduto.Produto.Should().Be(produto, "Deve ser igual ao produto que foi inserido");
}
```

#### Fixture
Uma fixture é uma classe que fica responsável por implementar códigos que serão reutilizados nos seus testes.
Além disso, a fixture tem a intenção de evitar que determinados dados sejam recriados a cada execução de teste
``` c#
public class ProtutoServiceFixture
{
    public AutoMocker Mocker;
    public ProdutoService Service;
    public ProtutoServiceFixture()
    {
        Mocker = new AutoMocker();
        Service = InstanciarService();
    }

    public ProdutoService InstanciarService() => Mocker.CreateInstance<ProdutoService>();
}
```
No exemplo acima, há propriedades e métodos que são comuns nos testes. Com isso, para evitar de ficar repetindo código e 'poluir' a classe de testes com estes códigos, a fixture fica responsável por implementar e compartilhá-los.

---

### Compartilhamento de dados entre os testes
A classe referente ao teste é instanciada a cada teste executado.
Há cenários que podemos reaproveitar dados entre os testes ou, até mesmo, ter alguma necessidade de compartilhar dados entre eles.
Como por exemplo, podemos reaproveitar instância de um serviço, repositório, etc.
Temos dois principais tipos de compartilhamentos de dados entre os testes:
- Compartilhar somente entre testes da mesma classe
- Compartilhar entre testes de classes distintas

#### Compartilhamentos dentro da mesma classe
Para compartilhar dados dentro da mesma classe de testes, podemos utilizar a interface ' IClassFixture ' , do próprio xUnit.
Esta interface espera uma classe como tipagem, e você insere a sua classe fixture para isso.
``` c#
public class ProtutoServiceFixture
{
    public AutoMocker Mocker;
    public ProdutoService Service;
    public ProtutoServiceFixture()
    {
        Mocker = new AutoMocker();
        Service = InstanciarService();
    }

    public ProdutoService InstanciarService() => Mocker.CreateInstance<ProdutoService>();
}

public class ProdutoServiceTests : IClassFixture<ProtutoServiceFixture>
{
  ProtutoServiceFixture _fixture;
  public ProdutoServiceTests(ProtutoServiceFixture fixture)
  {
      _fixture = fixture;
  }
}
```

#### Compartilhamentos em classes diferentes
Para compartilhar dados em classes diferentes, podemos utilizar a interface ' ICollectionFixture ' , do próprio xUnit.
Esta interface espera uma classe como tipagem, e você insere a sua classe fixture para isso.
``` c#
[CollectionDefinition(nameof(ProtutoServiceFixtureCollection))]
public class ProtutoServiceFixtureCollection : ICollectionFixture<ProtutoServiceFixture> { }
public class ProtutoServiceFixture
{
    public AutoMocker Mocker;
    public ProdutoService Service;
    public ProtutoServiceFixture()
    {
        Mocker = new AutoMocker();
        Service = InstanciarService();
    }

    public ProdutoService InstanciarService() => Mocker.CreateInstance<ProdutoService>();
}

[Collection(nameof(ProtutoServiceFixtureCollection))]
public class ProdutoServiceTests 
{
    ProtutoServiceFixture _fixture;
    public ProdutoServiceTests(ProtutoServiceFixture fixture)
    {
        _fixture = fixture;
    }
}
```

#### Geração de dados fakes
Nos testes, para termos mais certeza das validações e abranger mais cenários, surge a necessidade de gerar dados para simularmos o contexto daquele teste.
Para isso, podemos utilizar uma biblioteca específica para isso, chamada Bogus.
Ela é uma biblioteca bem completa e de fácil implementação.
Podemos utilizar uma instância comum ou uma instância tipada em caso de querermos dados fake de uma classe específica.
``` c#
 public CompraProduto CriarCompraProduto(string nomeProduto = null, double quantidade = 0)
{
    var faker = new Faker();
    var produto = new Produto()
        .DefinirPreco(faker.Random.Decimal())
        .DefinirNome(nomeProduto ?? faker.Random.String(20));
    return new CompraProduto()
        .DefinirQuantidade(quantidade == 0 ? faker.Random.Double() : quantidade)
        .DefinirProduto(produto);
}

public ICollection<CompraProduto> CriarCompraProdutoCollection(int quantidade = 1)
{
    var faker = new Faker();
    var fakerCompraProduto = new Faker<CompraProduto>();
    var compraProdutos = new List<CompraProduto>();
    fakerCompraProduto.RuleFor(x => x.Quantidade, faker.Random.Double());
    fakerCompraProduto.RuleFor(x => x.Produto, CriarProduto());
    return fakerCompraProduto.Generate(quantidade);
}
```
---

### Mock de classes
Ao implementar testes de unidade, muitas das vezes iremos testar uma classe que tem em sua instância outros objetos necessários - uma classe de serviço por exemplo.
Pensando nisso, para retirar essa complexibilidade de criação do objeto - e simular as injeções de depedências, por exemplo - utilizamos o mock destes objetos.
O mock fica responsável por criar uma classe complexa com todas as suas dependências devidamente 'implementadas'.
Para isso, foi utilizado o Moq e o AutoMock, duas bibliotecas práticas e boas.
No exemplo abaixo, o service irá ser criado com todas as suas DI devidamente simuladas.
``` c#
var mock = new AutoMocker();
var service = mock.CreateInstance<ProdutoService>();
```
---

### Asserções
No exemplo, utilizamos o FluentAssertion para realizar as asserções. Essa biblioteca deixa as asserções mais intituitivas, além de apresentar métodos de validações mais completos.
``` c#
[Trait("Categoria", "Produto")]
[Theory(DisplayName = "Definir nome")]
[InlineData("Produto 01")]
[InlineData("Produto 02")]
[InlineData("Produto 03")]
public void DefinirNome_NomeValido_DeveAlterarONome(string nome)
{
    // Act
    _produto.DefinirNome(nome);

    // Assert
    _produto.Nome.Should().Be(nome, "Produto de ter seu nome definido de acordo com o parâmetro passado");
}
```

###### Tecnologias utilizadas
- .NET 5
- Bogus
- coverlet.collector
- FluentAssertions
- Microsoft.NET.Test.Sdk
- Moq
- Moq.AutoMock
- xunit
- xunit.runner.visualstudio
