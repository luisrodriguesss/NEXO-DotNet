# NEXO — Laboratório de Decisões

### Módulo de Gerenciamento de Jogadores | .NET 10

**FIAP — Análise e Desenvolvimento de Sistemas**  
**Disciplina:** Advanced Business Development with .NET  
**Challenge 2026 — Sprint 1**

## 1. Integrantes

| Integrante | RM |
|---|---|
| [Luis Fillipe Rodrigues Seripieri] | [RM567918] |
| [Gabriel Rocha de Souza] | [RM567726] |
| [Luiz Kichimoto Valdevino] | [RM567023] |

**Turma:** [2TDSPB]

## 2. Visão geral

O **NEXO — Laboratório de Decisões** é um projeto educacional que propõe explorar as aplicações pacíficas da tecnologia nuclear por meio de cenários interativos e situações que envolvem tomada de decisões.

A proposta busca aproximar conceitos científicos do cotidiano e estimular o pensamento crítico sobre as aplicações da tecnologia nuclear em diferentes contextos.

O projeto está sendo desenvolvido de forma incremental, em três entregas acadêmicas.

### Papel do módulo .NET

Na primeira entrega, o módulo .NET é responsável pelo **gerenciamento de jogadores**, oferecendo uma aplicação de console para cadastrar e administrar os participantes do NEXO.

A solução funciona de maneira independente e estabelece uma base para futuras funcionalidades relacionadas à participação e ao progresso dos jogadores.

## 3. Funcionalidades implementadas

| Funcionalidade | Descrição |
|---|---|
| Cadastro | Registra nome de usuário e apelido |
| Listagem | Apresenta os jogadores cadastrados |
| Busca | Pesquisa parcial por nome de usuário ou apelido |
| Alteração de status | Ativa ou desativa jogadores |
| Edição de apelido | Atualiza o apelido de um jogador existente |
| Persistência | Salva e recupera os dados em JSON |
| Validações | Verifica campos obrigatórios e duplicidade |
| Tratamento de erros | Trata entradas inválidas e problemas de acesso ao arquivo |

### Menu da aplicação

```text
===== NEXO - LABORATÓRIO DE DECISÕES =====

1 - Cadastrar jogador
2 - Listar jogadores
3 - Buscar jogador
4 - Alterar status do jogador
5 - Editar apelido
6 - Sair
```

## 4. Entidade de domínio

A entidade principal do sistema é `Jogador`.

| Atributo | Tipo | Finalidade |
|---|---|---|
| `Id` | `int` | Identificador do jogador |
| `NomeUsuario` | `string` | Nome único utilizado no cadastro |
| `Apelido` | `string` | Nome de exibição do jogador |
| `DataCadastro` | `DateTime` | Data de criação do registro |
| `Ativo` | `bool` | Situação atual do jogador |

## 5. Regras de negócio

O sistema aplica regras que garantem a consistência das operações:

1. Nome de usuário e apelido são obrigatórios.
2. Nomes de usuário não podem se repetir, independentemente de letras maiúsculas ou minúsculas.
3. Cada jogador recebe um identificador numérico gerado automaticamente.
4. Novos jogadores são cadastrados com status ativo.
5. Um jogador não pode ser ativado se já estiver ativo, nem desativado se já estiver inativo.
6. A edição exige um apelido diferente do atual.
7. Operações de edição e alteração de status exigem um ID existente.
8. As alterações são persistidas no arquivo JSON.

Em caso de falha durante a gravação, os métodos de cadastro, edição e alteração de status restauram as modificações realizadas na memória.

## 6. Arquitetura e organização

O projeto possui três responsabilidades principais:

- **Apresentação:** interação com o usuário através do console.
- **Domínio:** representação da entidade Jogador.
- **Serviço:** validações, regras de negócio, consultas e persistência.

### Estrutura do repositório

```text
NEXO-DotNet/
├── Models/
│   └── Jogador.cs
├── Services/
│   └── JogadorService.cs
├── Program.cs
├── NEXO.DotNet.csproj
├── .gitignore
└── README.md
```

### Responsabilidades dos arquivos

**Program.cs**

Apresenta o menu, recebe os dados informados pelo usuário, chama as operações do serviço e exibe mensagens de sucesso ou erro.

**Models/Jogador.cs**

Define os atributos utilizados para representar cada jogador.

**Services/JogadorService.cs**

Implementa as funcionalidades de cadastro, listagem, busca, edição de apelido, ativação/desativação e leitura/gravação dos dados JSON.

**NEXO.DotNet.csproj**

Configura o projeto como aplicação executável utilizando o framework `net10.0`.

## 7. Tecnologias utilizadas

- C#
- .NET 10
- `System.Text.Json`
- LINQ
- Git
- GitHub
- Visual Studio Code

A persistência é realizada por arquivo JSON, sem dependência de banco de dados externo.

## 8. Como executar

### Pré-requisitos

Ter o **.NET SDK 10** instalado na máquina.

### Clonagem

```bash
git clone https://github.com/luisrodriguesss/NEXO-DotNet.git
```

### Acesso ao projeto

```bash
cd NEXO-DotNet
```

### Compilação

```bash
dotnet build
```

### Execução

```bash
dotnet run
```

Após iniciar, o programa apresenta o menu interativo. O usuário pode selecionar as funcionalidades digitando o número correspondente.

## 9. Persistência dos dados

O sistema utiliza o `System.Text.Json` para serializar e desserializar a coleção de jogadores.

Os dados são armazenados em um arquivo denominado `usuarios.json`, localizado na pasta de execução da aplicação.

Em uma execução padrão de desenvolvimento:

```text
bin/Debug/net10.0/usuarios.json
```

O arquivo é criado automaticamente durante o primeiro cadastro realizado com sucesso e permanece disponível para execuções posteriores.

Os diretórios `bin/` e `obj/` são ignorados pelo Git, assim como o arquivo `usuarios.json`.

### Compatibilidade com registros anteriores

A aplicação contempla a leitura de registros antigos que não possuem o atributo `Ativo`, atribuindo a eles o status ativo durante o carregamento.

## 10. Tratamento de erros

O sistema utiliza `try/catch` e validações para lidar com situações como:

- Campos obrigatórios não preenchidos.
- Nome de usuário duplicado.
- Jogador não encontrado.
- Tentativa de repetir um status existente.
- Tentativa de salvar um apelido igual ao atual.
- IDs inválidos.
- Problemas de leitura e gravação.
- Arquivo JSON com conteúdo inválido.

O objetivo é apresentar mensagens compreensíveis, evitando que erros previstos interrompam inesperadamente a experiência do usuário.

## 11. Testes realizados

Foram realizados testes manuais durante o desenvolvimento, incluindo:

| Teste | Resultado |
|---|---|
| Cadastro válido | Aprovado |
| Campos obrigatórios | Aprovado |
| Nome de usuário duplicado | Aprovado |
| Listagem de jogadores | Aprovado |
| Busca por nome ou apelido | Aprovado |
| Persistência após reinicialização | Aprovado |
| Leitura de JSON inválido | Aprovado |
| Ativação e desativação | Aprovado |
| Edição de apelido | Aprovado |

As funcionalidades foram executadas e verificadas localmente durante o desenvolvimento.

## 12. Como o .NET se aplica à solução do grupo

O .NET desempenha um papel complementar na arquitetura proposta para o NEXO, concentrando as funcionalidades de gerenciamento de jogadores e as regras de negócio associadas aos seus cadastros. Enquanto a API Java prevista no projeto ficará responsável pelos cenários, alternativas e decisões da experiência educacional, o módulo desenvolvido em C# oferece uma base independente para identificação e administração dos participantes. Essa separação de responsabilidades evita a duplicação de funcionalidades entre as tecnologias e facilita a evolução da solução nas próximas entregas. Nesta Sprint 1, o módulo .NET funciona como um protótipo de console com persistência local em JSON, sem integração efetiva com a API Java.

## 13. Evolução planejada

O projeto possui três entregas acadêmicas.

**Entrega 1 — Base funcional**

Gerenciamento de jogadores, validações, regras de negócio, persistência JSON e tratamento de erros.

**Entrega 2 — Evolução do módulo (proposta)**

Estudo de funcionalidades relacionadas à participação dos jogadores e ao acompanhamento de seu progresso na experiência educacional.

**Entrega 3 — Consolidação (proposta)**

Avaliação de integração com outros módulos do NEXO, aprimoramento da persistência e ampliação da cobertura de testes.

O planejamento poderá ser ajustado conforme os enunciados das próximas entregas.

## 14. Repositório

**GitHub:** https://github.com/luisrodriguesss/NEXO-DotNet

**Branch principal:** `main`

O desenvolvimento é registrado por commits que identificam a evolução das funcionalidades.

---

**Status:** Sprint 1 — Módulo .NET funcional e validado localmente.