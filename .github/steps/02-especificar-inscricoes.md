# Passo 2 — Transformar a necessidade em especificação

**Tempo sugerido: 20 minutos**

Trabalhe primeiro no contrato, não no código. Use o modo **Ask** ou **Plan** para analisar a
necessidade abaixo e revelar decisões que ainda estão implícitas:

> Permitir o cadastro de inscritos num curso, com nome, sobrenome e e-mail, sem cadastro
> separado de alunos, e cada aluno podendo ser inscrito apenas uma vez por curso.

1. Leia a especificação existente para reconhecer seu formato e seus limites.
2. Abra uma nova conversa no modo **Plan**.
3. Apresente somente a necessidade resumida e peça ao Copilot para levantar ambiguidades.
4. Discuta em equipe as decisões que afetam contrato ou comportamento observável.
5. Rejeite funcionalidades que não sejam necessárias para esta fatia.
6. Peça um rascunho de especificação, ainda sem alterar código.
7. Revise se cada regra pode ser comprovada por teste ou pela interface.
8. Salve o documento aprovado em `docs/specs/`.

A especificação deve ser curta, mas suficiente para orientar API, persistência, interface e
testes. Registre pelo menos:

- objetivo, escopo e itens fora do escopo;
- dados de entrada e regras de validação;
- como um treinamento é identificado;
- operações necessárias para cadastrar e visualizar inscritos;
- respostas para treinamento inexistente e e-mail duplicado;
- critérios de aceitação e evidências esperadas.

Questione sugestões que ampliem a atividade para turmas, cadastro global de alunos,
autenticação ou CRUD completo. Essas capacidades não fazem parte desta fatia.

> [!TIP]
> Para um prompt estruturado, decisões sugeridas e uma especificação pronta, consulte as
> [instruções completas](https://github.com/{{ repository }}/blob/main/.github/help/02-especificar-inscricoes.md).

Quando a equipe tiver revisado e aprovado a especificação, comente `especificado`.
