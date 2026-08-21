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

## 2. Diagnosticar o problema com o Copilot

1. Abra uma **nova conversa** no Chat.
2. Selecione o modo **Ask**.
3. Forneça como contexto:
   - `.github/copilot-instructions.md`;
   - `docs/specs/training-catalog-vertical-slice.md`;
   - `docs/specs/training-attendees-vertical-slice.md`.
4. Envie:

```text
Compare a seção "Especificação do catálogo" de `.github/copilot-instructions.md` com os
documentos atuais em `docs/specs/`.

Ainda não edite arquivos. Explique:
1. qual especificação a instrução atual manda ler;
2. por que essa referência fixa deixou de ser suficiente agora que existem duas fatias;
3. que erro de contexto pode ocorrer em uma tarefa sobre inscritos;
4. que erro de contexto pode ocorrer em uma futura tarefa sobre outro comportamento;
5. quais responsabilidades devem permanecer nas specifications e quais pertencem às
   repository instructions.
```

Leia a resposta. Confirme que o problema está na seleção fixa de uma única especificação, não
no conteúdo da especificação original.

## 3. Pedir uma proposta antes da edição

Na mesma conversa, envie:

```text
Proponha agora somente o trecho substituto para a seção "Especificação do catálogo".

O texto deve orientar o Copilot a identificar e ler em `docs/specs/` as especificações
relacionadas à solicitação atual, sinalizar conflitos antes de editar e exigir contrato
explícito para comportamentos novos.

Preserve propósito, plataforma, validação e todas as demais seções do arquivo. Não copie
regras detalhadas de produto para as instructions e não altere arquivos. Mostre:
1. o trecho atual;
2. o trecho proposto;
3. uma justificativa curta para cada mudança.
```

## 4. Revisar a proposta

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

## 5. Aplicar e revisar

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

## 6. Fazer um teste de contexto

Em uma nova conversa, pergunte:

```text
Quais especificações devem orientar a implementação do cadastro de inscritos e quais
contratos existentes não podem ser alterados?
```

A resposta deve localizar a especificação de inscritos e reconhecer que os contratos atuais
de treinamentos continuam válidos. Se o Copilot consultar apenas a especificação antiga,
revise a instrução.

## 7. Verificação final

- [ ] a referência deixou de ser fixa em uma única especificação;
- [ ] detalhes de produto continuam em `docs/specs/`;
- [ ] propósito, plataforma e validação foram preservados;
- [ ] o teste de contexto encontrou os documentos corretos;
- [ ] nenhum arquivo de `src` foi alterado.

Volte à issue e comente `contextualizado`.
