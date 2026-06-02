# Tracker

Aplicação de linha de comando (CLI) para rastreamento de estudos, desenvolvida em C# com .NET 10. Permite organizar módulos, tópicos, registros de estudo e metas diárias/mensais, com relatórios de evolução baseados em LINQ.

---

## 📋 Funcionalidades

- Organizar conteúdo de estudo em **Módulos** e **Tópicos**
- Registrar sessões de estudo com duração e data (**RegistroEstudo**)
- Acompanhar o progresso de cada módulo (% de tópicos concluídos)
- Definir **metas diárias** e **mensais** com verificação de cumprimento
- Gerar relatórios de evolução: streak de dias, tópicos em risco, horas por módulo
- Persistência de dados em JSON via `System.Text.Json`

---

## 🏗️ Arquitetura

O projeto segue uma separação de responsabilidades inspirada em **Domain-Driven Design (DDD)**, sem dependências externas de framework:

```
Tracker/
├── Domain/
│   ├── Entities/          # Modulo, Topico, MetaDiaria, MetaMensal
│   ├── Enums/             # StatusTopico
│   ├── Exceptions/        # TopicoDuplicadoException
│   ├── Interfaces/        # IMeta
│   └── ValueObjects/      # RegistroEstudo
├── Infrastructure/        # Persistencia (System.Text.Json)
└── UI/                    # Program.cs — menus e interações CLI
```

**Regra de dependência:** `UI` → `Domain` ← `Infrastructure`. A camada `Domain` não conhece `Infrastructure` nem `UI`.

---

## 🔧 Decisões Técnicas

### Imutabilidade em `RegistroEstudo`
`RegistroEstudo` é um Value Object imutável — todas as propriedades são somente leitura (`get` sem setter público). O `Id` é gerado via `Guid.NewGuid()` no construtor. Isso garante que um registro de estudo nunca seja alterado após sua criação.

### Encapsulamento em `Topico` e `Modulo`
As listas internas (`_registros`, `_topicos`) são campos privados expostos apenas via métodos que retornam `IReadOnlyList<T>`. O `Status` de um tópico só pode ser alterado pelo método `AlterarStatus()`, nunca diretamente.

### Enum `StatusTopico` em vez de strings
O uso do enum evita erros de digitação e comparações frágeis com strings. Toda a lógica de progresso e validação usa `StatusTopico.Concluido`, `StatusTopico.NaoIniciado`, etc.

### `IMeta` como contrato puro
A interface `IMeta` não possui nenhuma lógica — apenas propriedades e assinaturas de métodos. Isso permite que `MetaDiaria` e `MetaMensal` implementem critérios de cumprimento distintos sem acoplamento entre si.

### Persistência genérica
A classe `Persistencia` é genérica (`Salvar<T>` / `Carregar<T>`) e não conhece nenhuma classe de domínio. Isso mantém `Infrastructure` desacoplado de `Domain`.

### LINQ como ferramenta principal de consulta
`RelatorioDeEvolucao` e `Modulo.CalcularProgresso()` usam LINQ para agrupamentos, filtragens e agregações, sem loops imperativos desnecessários.

---

## 🚀 Como Executar

**Pré-requisitos:** [.NET 10 SDK](https://dotnet.microsoft.com/download)

```bash
# Clonar o repositório
git clone <url-do-repositorio>
cd Tracker

# Restaurar dependências e executar
dotnet run --project Tracker/Tracker.csproj
```

Para compilar sem executar:

```bash
dotnet build
```

---