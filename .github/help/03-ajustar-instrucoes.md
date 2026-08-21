# Passo 3 — Ajustar o contexto durável - Instruções completas

O objetivo é fazer o Copilot escolher as especificações relevantes para cada tarefa, sem
transformar `.github/copilot-instructions.md` em uma cópia dos requisitos do produto.

## 1. Identificar o problema

1. Abra `.github/copilot-instructions.md`.
2. Localize a seção sobre especificação do catálogo.
3. Observe que ela aponta diretamente para
   `docs/specs/training-catalog-vertical-slice.md`.
4. Liste os arquivos em `docs/specs/` e confirme que agora existem duas fatias.

A referência fixa funcionava quando havia um único documento. Com mais de uma fatia, ela pode
fazer o Copilot ignorar a especificação de inscritos ou aplicar regras de treinamentos fora do
contexto.

## 2. Pedir uma proposta antes da edição

Abra uma conversa no modo **Ask** ou **Plan**:

```text
Leia `.github/copilot-instructions.md` e liste os documentos em `docs/specs/`.

A seção de especificação referencia sempre uma única fatia, mas o repositório agora possui
comportamentos independentes. Não edite ainda.

Apresente:
1. o problema causado pela referência fixa;
2. o menor trecho substituto que mande selecionar as especificações relevantes à tarefa;
3. como o texto preserva contrato explícito e sinalização de conflitos;
4. por que regras detalhadas de inscritos não devem ser copiadas para as instructions.

Preserve propósito, plataforma e validação existentes.
```

## 3. Revisar a proposta

Aceite apenas uma proposta que:

- mande consultar `docs/specs/` antes de planejar mudanças de comportamento;
- selecione documentos relacionados à solicitação atual;
- exija que conflitos sejam apresentados antes de editar;
- exija contrato explícito para comportamento novo;
- preserve as instruções de .NET 10, Codespaces e validação;
- não mencione detalhes como rotas, campos ou status de inscritos.

Uma formulação adequada pode orientar:

```markdown
## Especificações do catálogo

Antes de planejar ou alterar o comportamento do catálogo, identifique e leia em `docs/specs/`
as especificações relacionadas à solicitação atual. Se a solicitação conflitar com um contrato
aprovado, sinalize o conflito antes de editar. Novos comportamentos exigem contrato explícito
e não podem alterar silenciosamente critérios existentes.
```

Use esse texto como referência, não como substituição obrigatória da análise.

## 4. Aplicar e revisar

1. Autorize o Copilot a editar somente `.github/copilot-instructions.md`.
2. Abra **Source Control** na barra lateral do VS Code.
3. Selecione `.github/copilot-instructions.md` para abrir a comparação lado a lado.
4. Confirme nas linhas removidas e adicionadas que apenas a seção necessária mudou.
5. Verifique que os links e instruções restantes continuam corretos.
6. Se preferir o terminal, execute:

   ```bash
   git diff -- .github/copilot-instructions.md
   git diff --check
   ```

## 5. Fazer um teste de contexto

Em uma nova conversa, pergunte:

```text
Quais especificações devem orientar a implementação do cadastro de inscritos e quais
contratos existentes não podem ser alterados?
```

A resposta deve localizar a especificação de inscritos e reconhecer que os contratos atuais
de treinamentos continuam válidos. Se o Copilot consultar apenas a especificação antiga,
revise a instrução.

## 6. Verificação final

- [ ] a referência deixou de ser fixa em uma única especificação;
- [ ] detalhes de produto continuam em `docs/specs/`;
- [ ] propósito, plataforma e validação foram preservados;
- [ ] o teste de contexto encontrou os documentos corretos;
- [ ] nenhum arquivo de `src` foi alterado.

Volte à issue e comente `contextualizado`.
