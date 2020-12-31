<h2 align="center">DesignPatternSamples</h2>

___




<h3 align="center">
  <a href="#about">Sobre</a>&nbsp;|&nbsp;
  <a href="#reason">Motivo</a>&nbsp;|&nbsp;
  <a href="#requirements">Requisitos</a>&nbsp;|&nbsp;
  <a href="#technologies">Tecnologias</a>&nbsp;|&nbsp;
	<a href="#references">Referências</a>&nbsp;|&nbsp;
</h3>

___


<h2 id="about">🔎 Sobre</h2>

Projeto aplicando Design Patterns na prática em um projeto WebAPI .NET Core 3.1 utilizada na palestra "Aplicando design patterns na prática com C#" <a href="https://github.com/fructuoso">By Victor Fructuoso</a>

<h2 id="reason">🎯 Motivo</h2>

O objetivo aqui é aprender, praticar e comprovar minhas habilidades em Design Patterns. Desenvolvidas durante o Bootcamp Decola Dev Avanade 2021, que foi proposta através da DigitalInnovationOne.

<h2 id="requirements">⚙ Requisitos</h2>

git clone ou baixar esse repositório, depois disso:

1. 
```bash
dotnet clean
dotnet build
dotnet run
```
E utilizar uma ferramenta, como o Postman, para realizar requisições HTTP. 😊

Ou utilizar o swagger para fazer seus Testes 😊

<h2 id="technologies">🚀 Tecnologias</h2>

O projeto foi desenvolvido em:

- .NET Core 3.1


<h2 id="references">📚 Referências</h2>
<h3> Vou deixar aqui os Design Patterns que o autor escreveu em seu repositório original -> <a href="https://github.com/fructuoso">Victor Fructuoso</a></h3>


## Padrões na Prática

### Strategy

#### Problema:

Nosso objetivo é Utilizar o método Distinct do System.Linq, este por sua vez espera como entrada uma IEqualityComparer. Isso por si só já representa uma implementação de Strategy, entretanto nós não queremos criar uma única implementação engessada que nos permita comparar um determinado objeto de uma única forma.

##### Solução:

1. Criar uma classe que implemente a interface [IEqualityComparer](https://docs.microsoft.com/pt-br/dotnet/api/system.collections.generic.iequalitycomparer-1?view=netcore-3.1);
2. Esta classe deve receber o 'como' os objetos deverão ser comparados através de um parâmetro, que neste caso é uma função anônima;

Desta forma a classe que criamos sabe comparar objetos, porém ela não sabe os critérios que serão utilizados, os critérios serão injetados através de uma função anônima.

[Implementação](src/Workbench.Comparer/GenericComparerFactory.cs)\
[Consumo](src/Workbench.GenericComparer.Tests/GenericComparerFactoryTest.cs#L27)

Podemos tornar o consumo ainda mais interessante criando uma *Sugar Syntax* através de métodos de extensão.

[Implementação](src/Workbench.Linq.Extensions/DistinctExtensions.cs)\
[Consumo](src/Workbench.Linq.Extensions.Tests/DistinctExtensionsTests.cs#L26)

Desta forma através do padrão [Strategy](#strategy) estamos aderentes ao princípio **Aberto-Fechado** e **Inversão de Controle**.

### Factory

#### Problema: 

Nós queremos criar um serviço de consulta de débitos do veículo que seja capaz de acessar o sistema do DETRAN e obter estas informações, porém o DETRAN possui uma aplicação completamente diferente de acordo com o estado.

Nós não queremos modificar o nosso serviço sempre que um novo estado for implementado.

#### Solução:

1. Criar uma interface que determine uma assinatura única para o serviço;
2. Realizar uma implementação para cada um dos estados;
3. Criar uma classe Factory, onde sua responsabilidade será determinar qual classe concreta deverá ser instanciada;

[Implementação](src/Infra.Repository.Detran/DetranVerificadorDebitosFactory.cs)\
[Consumo](src/Application/Implementations/DetranVerificadorDebitosServices.cs#L20)\
[Teste](src/Infra.Repository.Detran.Tests/DetranVerificadorDebitosFactoryTests.cs#L22)

Desta forma através do padrão [Factory](#factory) estamos aderentes ao princípio **Aberto-Fechado**.

Neste exemplo o nosso [Factory](#factory) ainda está diretamente relacionado ao princípio **Substituição de Liskov**.

### Singleton

#### Problema:

Visto que o nosso Factory tem como responsabilidade apenas identificar a classe concreta que teve ser inicializada a partir de um Setup pré-estabelecido no [Startup](src/WebAPI/Startup.cs#L130) da aplicação, não faz sentido que ele seja instanciado a cada solicitação.

#### Solução:

Como estamos fazendo uso da Injeção de Dependência nativa do .Net Core processo se torna mais simples:

1. Modificar o registro no Startup para que o serviço seja registrado como Singleton.

[Implementação](src/WebAPI/Startup.cs#L111)

Com isso nós temos uma única instância sendo inicializada e configurada no [Startup](src/WebAPI/Startup.cs#L130) da aplicação.

### Template Method

#### Problema:

Visto que em algum momento iremos implementar 27 serviços diferentes para acessar os DETRAN que temos espalhados pelo Brasil.

Entendemos que, mesmo que, os sites sejam diferentes, os passos necessários para extrair a informação costumam ser semelhantes:

* Consultar Site
* Consolidar Resultado

Como a nossa interface [IDetranVerificadorDebitosRepository](src/Application/Repository/IDetranVerificadorDebitosRepository.cs) possui apenas o método ConsultarDebitos, nosso código corre risco de não ficar padronizado e ainda perdermos o princípio da **Responsabilidade Única**.

#### Solução:

1. Criar uma classe abstrata com métodos mais específicos para realizar o trabalho desejado;
2. A classe abstrata 'deve' implementar o método exposto pela Interface;
3. Ao invés das classes implementarem a Interface, elas herdarão o comportamento da classe abstrata, implementando apenas os métodos mais específicos.

Com isso torna-se mais fácil:
* Dividir o trabalho;
* Testar o código.

[Implementação](src/Infra.Repository.Detran/DetranVerificadorDebitosRepositoryCrawlerBase.cs)\
[Consumo](src/Infra.repository.detran/DetranPEVerificadorDebitosRepository.cs)

O neste exemplo o nosso [Template Method](#template-method) ainda seguindo o princípio **Segregação da Interface**, onde os métodos específicos foram adicionados na nossa classe abstrata [DetranVerificadorDebitosRepositoryCrawlerBase](src/Repository.Detran/../Infra.Repository.Detran/DetranVerificadorDebitosRepositoryCrawlerBase.cs), desta forma conseguimos atingir também o princípio de **Substituição de Liskov**.

### Decorator

#### Problema: 

Com o serviço [DetranVerificadorDebitosServices](src/Application/Implementations/DetranVerificadorDebitosServices.cs) identificamos que precisamos adicionar funcionalidades técnicas a ele (como por exemplo **Log** e **Cache**), porém essas funcionalidades não devem gerar acoplamento no nosso código.

Então como fazer isso sem quebrar os princípios **Responsabilidade Única** e **Aberto-Fechado**?

#### Solução:

Neste cenário estamos usando uma abordagem que nos permite transferir a complexidade de registrar um Decorator no ServiceCollection para um método de extensão.

Desta forma precisamos:

1. Criar uma nova classe concreta que deverá implementar a Interface que será 'decorada';
2. Implementar nesta nova classe a funcionalidade que gostaríamos de acrescentar ao método em questão;
3. Adicionar Decorator no Injetor de Dependências fazendo referência à interface que será decorada.

Obs.: É possível incluir mais de um Decorator, porém é preciso ter ciência de que a ordem em que eles são associados faz diferença no resultado final.

[Método de Extensão](src/Workbench.DependencyInjection.Extensions/ServiceCollectionExtensions.cs#L10)\
[Implementação](src/Application/Decorators/DetranVerificadorDebitosDecoratorLogger.cs#L23)\
[Registro](src/WebAPI/Startup.cs#L110)

O Decorator funciona como uma 'Boneca Russa' dessa forma podemos 'empilhar' diversos Decorators em uma mesma Interface.

Temos o exemplo de um segundo Decorator adicionando o recurso de Cache ao nosso Service.

[Implementação](src/Application/Decorators/DetranVerificadorDebitosDecoratorCache.cs#L25)\
[Registro](src/WebAPI/Startup.cs#L09)

Desta forma nós agregamos duas funcionalidades ao nosso serviço sem modificar o comportamento do serviço, ou modificar quem chama o serviço, desta forma estamos aderentes aos princípios **Responsabilidade Única**, **Aberto-Fechado** e **Inversão de Controle**.

<u>Obs.: Seguir o princípio Segregação de Interfaces pode tornar o seu Decorator mais simples de ser implementado, visto que você terá menos métodos para submeter ao padrão.</u>