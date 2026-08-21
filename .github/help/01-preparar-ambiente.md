# Passo 1 — Preparar o ambiente - Instruções completas

Use este roteiro se precisar de orientação detalhada para preparar e reconhecer o projeto.
Não implemente inscritos neste passo.

## 1. Criar o Codespace

1. Volte ao comentário principal do passo 1.
2. Clique em **Abrir no GitHub Codespaces**.
3. Na tela de criação, confirme que o repositório copiado por você está selecionado.
4. Crie o Codespace e aguarde o VS Code abrir no navegador.
5. Espere o `postCreateCommand` terminar. Ele instala o SQLite e apresenta informações do .NET.
6. Se o terminal não estiver visível, abra **Terminal > New Terminal**.

## 2. Selecionar o ponto de partida

> [!CAUTION]
> Não pule esta seção. A branch `inicio` contém o ponto de partida preparado para o lab.
> Trabalhar na `main` comprometerá toda a atividade.

Execute:

```bash
git switch inicio
git branch --show-current
git status --short
```

O primeiro comando seleciona a branch preparada para o exercício. O segundo deve imprimir
exatamente `inicio`. O terceiro não deve mostrar alterações locais.

Se `git branch --show-current` mostrar `main`, execute `git switch inicio` novamente antes de
continuar. Se a branch `inicio` não existir, interrompa a atividade: a cópia do template
provavelmente foi criada sem **Include all branches**.

## 3. Confirmar as ferramentas

```bash
dotnet --version
sqlite3 --version
```

O .NET deve informar uma versão 10.x. O SQLite deve imprimir sua versão, e não
`command not found`.

## 4. Validar a linha de base

Execute um comando por vez:

```bash
dotnet restore src/TrainingCatalog.slnx
dotnet build src/TrainingCatalog.slnx --no-restore
dotnet test src/TrainingCatalog.slnx --no-build
```

O `restore` recupera dependências, o `build` compila todos os projetos e o `test` executa a
suíte existente. Se um comando falhar:

1. não altere código imediatamente;
2. copie o comando e a primeira mensagem de erro relevante;
3. confirme que o comando anterior terminou com sucesso;
4. discuta com a equipe se a falha é ambiental ou pertence à linha de base;
5. peça ajuda ao instrutor se não conseguir estabelecer uma linha de base confiável.

## 5. Reconhecer a solução

Abra os itens abaixo no Explorer do VS Code:

| Caminho | O que observar |
| --- | --- |
| `docs/specs/training-catalog-vertical-slice.md` | como o comportamento aprovado é documentado |
| `.github/copilot-instructions.md` | instruções carregadas automaticamente pelo Copilot |
| `src/Application` | contratos compartilhados entre API e Client |
| `src/Api/Program.cs` | rotas, validações e respostas HTTP |
| `src/Infrastructure` | entidade, `DbContext` e migrations |
| `src/Client/Pages` | formulário e lista atuais em Blazor |
| `src/Tests/Api.Tests` | testes funcionais pela API pública |

Para cada pasta, responda em equipe: "qual parte da nova fatia provavelmente passará por
aqui?". Não é necessário decidir arquivos ou classes ainda.

## 6. Verificação final

Antes de voltar à issue, confirme:

- [ ] `git branch --show-current` imprime `inicio`;
- [ ] .NET 10 e SQLite estão disponíveis;
- [ ] restore, build e testes foram executados;
- [ ] sei onde ficam especificação, contracts, API, persistência, UI e testes;
- [ ] nenhum arquivo foi alterado.

Volte à issue e comente `preparado`.
