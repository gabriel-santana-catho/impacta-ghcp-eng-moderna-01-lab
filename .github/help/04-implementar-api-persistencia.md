# Passo 4 — Implementar API e persistência - Instruções completas

Esta é a etapa mais longa do lab. Trabalhe em incrementos e valide cada decisão antes de
avançar. A especificação aprovada pela equipe é a autoridade.

## 1. Preparar uma conversa limpa

1. Abra uma nova conversa no modo **Agent**.
2. Adicione ao contexto:
   - `.github/copilot-instructions.md`;
   - `docs/specs/training-attendees-vertical-slice.md`;
   - `docs/specs/training-catalog-vertical-slice.md`;
   - `src/Application`;
   - `src/Api/Program.cs`;
   - `src/Infrastructure`;
   - `src/Tests/Api.Tests`.
3. Não inclua os arquivos do Client: a interface será tratada no passo seguinte.

Uma conversa nova reduz a chance de o Copilot continuar decisões exploratórias da etapa de
especificação como se já fossem aprovadas.

## 2. Solicitar o plano

Envie:

```text
Leia as instruções e as especificações relevantes. Inspecione os padrões existentes em
Application, Api, Infrastructure e Api.Tests.

Planeje, sem editar, a implementação da fatia aprovada de cadastro e listagem de inscritos.

O plano deve apresentar:
1. contratos públicos de requisição e resposta;
2. rotas e resultados HTTP;
3. entidade, relacionamento e chave estrangeira;
4. índice que garante unicidade por treinamento;
5. estratégia única de normalização do e-mail;
6. arquivos que serão criados ou alterados;
7. testes pela API pública;
8. sequência de implementação e comandos de validação.

Restrições:
- preserve todos os contratos existentes de treinamentos;
- use Entity Framework Core e SQLite já configurados;
- não crie cadastro global de aluno;
- não imponha unicidade global do e-mail;
- mantenha detalhes de persistência fora dos contratos públicos;
- não implemente edição, exclusão, autenticação, paginação ou interface;
- gere uma migration, mas não a aplique antes da revisão.

Pare depois do plano e aguarde minha aprovação.
```

## 3. Revisar o plano

Use esta tabela:

| Ponto | O que deve estar claro |
| --- | --- |
| rota | `trainingId` identifica o treinamento na URL |
| corpo | contém nome, sobrenome e e-mail, sem repetir `trainingId` |
| inexistência | cadastro e listagem retornam `404` |
| validação | campos inválidos retornam `400` com `errors` |
| duplicidade | mesmo e-mail no mesmo treinamento retorna `409` |
| normalização | aplicação e restrição do banco usam a mesma representação |
| relacionamento | inscrito depende de um treinamento existente |
| testes | usam a API pública e SQLite isolado |

Peça ajustes se o plano criar repositórios genéricos, serviços sem necessidade, cadastro de
alunos ou mais operações do que a especificação exige.

## 4. Implementar contratos e modelo

Depois de aprovar o plano, autorize o primeiro incremento:

```text
Implemente primeiro somente os contratos compartilhados e o modelo de persistência aprovados.
Configure relacionamento e índice composto no DbContext. Ainda não gere a migration.

Ao terminar:
1. mostre os arquivos alterados;
2. explique como o índice representa a regra;
3. execute o build;
4. pare para revisão.
```

Revise:

- o DTO público não expõe campos internos de normalização;
- a entidade possui a chave estrangeira necessária;
- o índice combina treinamento e e-mail normalizado;
- o e-mail não ficou único globalmente;
- exclusão em cascata, se configurada, foi uma decisão consciente.

## 5. Implementar os endpoints

Continue:

```text
Implemente agora os endpoints de cadastro e listagem conforme a especificação.

Reutilize o formato de erros existente. Garanta que:
- o treinamento seja verificado;
- a entrada seja validada antes de persistir;
- o e-mail seja normalizado de forma consistente;
- conflito de unicidade produza o contrato 409 aprovado;
- a resposta de criação informe a localização;
- a listagem pertença somente ao treinamento da rota.

Execute o build e pare antes dos testes. Abra **Source Control** no VS Code e selecione cada
arquivo alterado para revisar sua comparação. Como alternativa, apresente `git diff` no
terminal.
```

Confira o contrato esperado:

| Operação | Rota | Resultado |
| --- | --- | --- |
| cadastrar | `POST /api/trainings/{trainingId}/attendees` | `201`, `400`, `404` ou `409` |
| listar | `GET /api/trainings/{trainingId}/attendees` | `200` ou `404` |

## 6. Criar os testes

Peça:

```text
Adicione testes funcionais pela API pública, seguindo a factory e o isolamento SQLite
existentes. Cubra somente:
1. cadastro válido e localização;
2. listagem após cadastro;
3. nome, sobrenome e e-mail inválidos;
4. treinamento inexistente no POST e GET;
5. duplicidade com variações de caixa e espaços;
6. confirmação de que a duplicidade não armazenou outro item;
7. mesmo e-mail permitido em treinamentos diferentes.

Execute primeiro somente os novos testes. Se falharem, investigue antes de alterar o contrato.
```

Execute também manualmente, se necessário:

```bash
dotnet test src/Tests/Api.Tests/TrainingCatalog.Api.Tests.csproj
```

Leia as falhas. Não aceite uma correção que enfraqueça a especificação apenas para fazer o
teste passar.

## 7. Gerar e revisar a migration

Confirme antes se a ferramenta está disponível:

```bash
dotnet ef --version
```

Gere a migration:

```bash
dotnet ef migrations add AddTrainingAttendees \
  --project src/Infrastructure \
  --startup-project src/Api
```

Não execute `database update` ainda. Peça:

```text
Use a skill `review-ef-migration` para revisar a migration recém-gerada.

Verifique:
- tabela e colunas;
- nulabilidade;
- chave primária;
- chave estrangeira;
- comportamento de exclusão;
- índice composto de unicidade;
- alterações inesperadas em tabelas existentes;
- coerência do snapshot.

Não aplique nem edite até apresentar os achados.
```

Abra os arquivos da migration e confirme os achados do Copilot.

## 8. Aplicar e validar

Depois de aprovar:

```bash
dotnet ef database update \
  --project src/Infrastructure \
  --startup-project src/Api

dotnet build src/TrainingCatalog.slnx
dotnet test src/TrainingCatalog.slnx --no-build
```

Opcionalmente, inspecione a estrutura:

```bash
sqlite3 src/Api/training-catalog.db ".schema"
```

Procure a tabela de inscritos, a chave estrangeira e o índice composto.

## 9. Verificação final

- [ ] contratos públicos correspondem à especificação;
- [ ] todos os endpoints antigos continuam compilando e passando nos testes;
- [ ] cadastro e listagem possuem testes pela API pública;
- [ ] duplicidade por treinamento está protegida na aplicação e no banco;
- [ ] o mesmo e-mail é permitido em treinamentos diferentes;
- [ ] migration e snapshot foram revisados;
- [ ] nenhuma funcionalidade de interface foi implementada.

Volte à issue e comente `persistido`.
