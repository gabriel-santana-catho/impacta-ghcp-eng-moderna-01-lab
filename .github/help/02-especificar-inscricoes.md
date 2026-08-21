# Passo 2 — Transformar a necessidade em especificação - Instruções completas

Neste passo você transformará uma frase de negócio em um contrato que possa orientar código e
validação. Não implemente API, banco ou interface ainda.

## 1. Ler o exemplo existente

Abra `docs/specs/training-catalog-vertical-slice.md` e identifique:

1. objetivo e limites da fatia;
2. dados e regras;
3. contratos HTTP;
4. comportamento esperado da interface;
5. critérios de aceitação;
6. evidências que comprovam os critérios.

Use a estrutura como referência, mas não copie regras específicas de treinamentos para
inscritos.

## 2. Explorar as ambiguidades

Abra uma nova conversa no modo **Plan** e envie:

```text
Leia `docs/specs/training-catalog-vertical-slice.md` apenas para entender o formato e os
contratos existentes.

Analise esta necessidade:
"Permitir o cadastro de inscritos num curso, com nome, sobrenome e e-mail, sem cadastro
separado de alunos, e cada aluno podendo ser inscrito apenas uma vez por curso."

Ainda não edite arquivos nem implemente código.

Liste somente as ambiguidades que impedem um contrato verificável. Para cada uma:
1. explique por que ela afeta API, persistência, interface ou teste;
2. apresente a opção mais simples coerente com o projeto atual;
3. destaque quando a decisão amplia o escopo.

Mantenha fora do escopo turmas, cadastro global de alunos, autenticação, paginação e CRUD
completo.
```

Leia a resposta antes de prosseguir. O Copilot deve ajudar a revelar decisões, não tomá-las
silenciosamente.

## 3. Aprovar decisões simples

Para este lab, uma combinação adequada é:

| Tema | Decisão sugerida |
| --- | --- |
| vínculo | `trainingId` pertence à rota e não é repetido no corpo |
| cadastro | `POST /api/trainings/{trainingId}/attendees` |
| consulta | `GET /api/trainings/{trainingId}/attendees` |
| identidade | o sistema gera `id` para o inscrito |
| campos | `firstName`, `lastName` e `email` obrigatórios |
| e-mail | remover espaços externos e comparar sem diferença de caixa |
| treinamento ausente | responder `404 Not Found` |
| duplicidade | responder `409 Conflict` e identificar `email` |
| alcance da unicidade | o mesmo e-mail pode existir em treinamentos diferentes |
| interface | somente cadastro e listagem |

Discuta as decisões em equipe. Em seguida, **informe explicitamente as decisões ao Copilot por
meio de um novo prompt**. Não presuma que ele interpretará a discussão da equipe ou escolherá
automaticamente as opções da tabela.

Use este prompt, ajustando qualquer decisão que sua equipe tenha tomado de forma diferente:

```text
Para esta fatia, nossa equipe aprovou as seguintes decisões:

- o identificador do treinamento será `trainingId` e ficará somente na rota;
- o cadastro usará `POST /api/trainings/{trainingId}/attendees`;
- a listagem usará `GET /api/trainings/{trainingId}/attendees`;
- o sistema gerará o identificador do inscrito;
- `firstName`, `lastName` e `email` serão obrigatórios;
- o e-mail terá os espaços externos removidos e será comparado sem diferença entre letras
  maiúsculas e minúsculas;
- treinamento inexistente produzirá `404 Not Found`;
- e-mail repetido no mesmo treinamento produzirá `409 Conflict` com erro associado a `email`;
- o mesmo e-mail poderá ser inscrito em treinamentos diferentes;
- a interface oferecerá somente cadastro e listagem de inscritos;
- turmas, cadastro global de alunos, edição, exclusão, autenticação e paginação continuarão
  fora do escopo.

Considere essas decisões aprovadas nos próximos passos. Antes de redigir a especificação,
aponte somente se alguma delas for contraditória, insuficiente para um contrato verificável ou
incompatível com um contrato existente. Ainda não edite arquivos.
```

Leia a resposta e resolva qualquer conflito apontado. Se sua equipe escolher algo diferente,
altere o prompt e mantenha os critérios posteriores coerentes com essa decisão.

## 4. Pedir o rascunho

Continue na mesma conversa:

```text
Com base nas decisões aprovadas, proponha o conteúdo de
`docs/specs/training-attendees-vertical-slice.md`.

Inclua:
- estado;
- objetivo;
- escopo e fora do escopo;
- tabela de dados do inscrito;
- contrato de cadastro;
- contrato de listagem;
- comportamento da interface;
- critérios de aceitação numerados;
- tabela de evidências esperadas;
- decisões ainda abertas.

Use linguagem de comportamento observável. Não prescreva nomes de classes, organização
interna ou detalhes do Entity Framework Core. Mostre o documento antes de editar.
```

## 5. Revisar o rascunho

Confirme:

- cada campo obrigatório possui resultado esperado quando inválido;
- a especificação distingue treinamento inexistente de entrada inválida;
- duplicidade é definida por treinamento;
- caixa e espaços do e-mail não permitem contornar a regra;
- o mesmo e-mail em outro treinamento é permitido;
- a interface possui estados de carregamento, sucesso, vazio e erro;
- cada critério possui uma evidência possível;
- nada exige turmas ou cadastro separado de alunos.

Depois da revisão, envie este prompt:

```text
Aprovo o rascunho revisado.

Salve o conteúdo exatamente em
`docs/specs/training-attendees-vertical-slice.md`, a partir da raiz deste repositório.

Crie o diretório somente se ele não existir. Não altere
`docs/specs/training-catalog-vertical-slice.md`, `.github/copilot-instructions.md` nem qualquer
arquivo em `src`.

Depois de salvar:
1. informe o caminho completo do arquivo criado;
2. mostre um resumo do conteúdo salvo;
3. confirme que nenhum outro arquivo foi alterado.
```

Abra `docs/specs/training-attendees-vertical-slice.md` no Explorer do VS Code e confira se o
conteúdo salvo corresponde ao rascunho aprovado.

## 6. Especificação pronta para contingência

Se o grupo estiver bloqueado ou sem tempo, use o documento abaixo como referência. Revise-o
antes de salvar; copiar sem compreender elimina o objetivo deste passo.

```markdown
# Especificação — Inscritos em treinamentos

## Estado

- Status: aprovado
- Responsáveis: equipe do lab
- Última revisão: preencher ao versionar

## Objetivo

Permitir que uma pessoa responsável consulte e cadastre inscritos diretamente em um
treinamento existente, sem manter um cadastro separado de alunos.

## Escopo

- cadastrar um inscrito em um treinamento;
- validar nome, sobrenome e e-mail;
- impedir a repetição do mesmo e-mail no mesmo treinamento;
- persistir os inscritos;
- listar os inscritos de um treinamento;
- acessar o gerenciamento de inscritos pela lista de treinamentos;
- representar sucesso e falhas pela interface.

## Fora do escopo

- turmas;
- cadastro global de alunos;
- inscrição do mesmo aluno em várias turmas;
- edição ou exclusão de inscritos;
- autenticação e autorização;
- paginação, busca e ordenação;
- envio de mensagens ou confirmação por e-mail.

## Dados do inscrito

| Campo | Tipo | Regra |
| --- | --- | --- |
| `id` | identificador | gerado pelo sistema |
| `firstName` | texto | obrigatório e não vazio |
| `lastName` | texto | obrigatório e não vazio |
| `email` | texto | obrigatório e com formato válido |
| `trainingId` | identificador | obtido pela rota e deve apontar para treinamento existente |

Para verificar duplicidade, o e-mail deve desconsiderar espaços externos e diferenças entre
letras maiúsculas e minúsculas. O mesmo e-mail pode ser usado em treinamentos diferentes.

## Contrato da API

### Cadastrar inscrito

- Método e rota: `POST /api/trainings/{trainingId}/attendees`
- Corpo: `firstName`, `lastName` e `email`
- Sucesso: `201 Created`, representação do inscrito e localização do recurso
- Dados inválidos: `400 Bad Request` no formato `{ "errors": { "campo": ["mensagem"] } }`
- Treinamento inexistente: `404 Not Found`
- E-mail já inscrito no treinamento: `409 Conflict` com erro associado a `email`

### Listar inscritos

- Método e rota: `GET /api/trainings/{trainingId}/attendees`
- Sucesso: `200 OK` com uma coleção; a coleção pode estar vazia
- Treinamento inexistente: `404 Not Found`

## Comportamento da interface

- a lista de treinamentos oferece uma ação para abrir seus inscritos;
- a página identifica o treinamento selecionado;
- a página exibe carregamento e lista vazia;
- o formulário coleta nome, sobrenome e e-mail;
- um envio em andamento não pode ser repetido;
- o sucesso apresenta confirmação e atualiza a lista;
- uma falha apresenta mensagem útil sem apagar os dados preenchidos.

## Critérios de aceitação

1. Dados válidos para um treinamento existente produzem `201` e o inscrito aparece na lista.
2. Nome ausente produz `400` e identifica `firstName`.
3. Sobrenome ausente produz `400` e identifica `lastName`.
4. E-mail ausente ou inválido produz `400` e identifica `email`.
5. Treinamento inexistente produz `404` no cadastro e na listagem.
6. Repetir no mesmo treinamento um e-mail com variação de caixa ou espaços produz `409`.
7. A duplicidade não armazena um segundo inscrito.
8. O mesmo e-mail pode ser cadastrado em treinamentos diferentes.
9. A interface é acessível pela lista de treinamentos.
10. A interface apresenta sucesso e atualiza a lista após o cadastro.
11. A interface preserva os campos e apresenta mensagem útil em caso de erro.

## Evidências esperadas

| Critério | Evidência mínima |
| --- | --- |
| validação de entrada | resposta HTTP e teste automatizado |
| cadastro válido | resposta `201` e teste automatizado |
| treinamento inexistente | resposta `404` e teste automatizado |
| unicidade por treinamento | resposta `409`, teste e restrição no banco |
| mesmo e-mail em outro treinamento | teste automatizado |
| persistência e listagem | consulta após cadastro |
| acesso pela lista | fluxo executado no navegador |
| sucesso e erro na interface | ambos os fluxos executados no navegador |

## Decisões ainda abertas

- organização interna dos contratos e entidades;
- detalhes visuais da página;
- estratégia adicional de testes além das evidências mínimas.
```

## 7. Verificação final

- [ ] o arquivo está em `docs/specs/training-attendees-vertical-slice.md`;
- [ ] o status está aprovado;
- [ ] critérios e evidências correspondem;
- [ ] não houve alteração em `src`;
- [ ] a equipe compreende as decisões adotadas.

Volte à issue e comente `especificado`.
