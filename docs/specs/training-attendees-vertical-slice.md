# Especificação — Fatia vertical de inscritos em treinamentos

## Estado

- Status: aprovado
- Responsáveis: turma e instrutor
- Última revisão: preencher ao aprovar

## Objetivo

Permitir que uma pessoa responsável inscreva alguém em um treinamento existente e consulte, pela interface, os inscritos daquele treinamento.

Cada inscrito será registrado diretamente na inscrição, sem cadastro separado de alunos. O mesmo endereço de e-mail poderá aparecer em treinamentos diferentes, mas não poderá ser inscrito mais de uma vez no mesmo treinamento.

## Escopo

- receber dados de um inscrito pela API;
- associar o inscrito a um treinamento existente;
- exigir nome, sobrenome e e-mail;
- remover espaços externos do e-mail antes de armazená-lo;
- comparar e-mails sem diferenciar letras maiúsculas e minúsculas;
- rejeitar uma segunda inscrição com o mesmo e-mail no mesmo treinamento;
- consultar os inscritos de um treinamento pela API;
- oferecer uma interface para cadastrar e listar inscritos;
- produzir evidências automatizadas dos comportamentos principais.

## Fora do escopo desta fatia

- turmas;
- cadastro global de alunos;
- autenticação e autorização;
- paginação;
- busca e ordenação;
- edição de inscritos;
- exclusão de inscritos;
- CRUD completo de alunos ou inscritos;
- regras de capacidade do treinamento;
- confirmação externa do endereço de e-mail;
- escolha definitiva do provedor de banco de dados;
- requisitos de produção, observabilidade e alta disponibilidade.

## Dados do inscrito

| Campo | Tipo | Regra |
| --- | --- | --- |
| `id` | identificador | gerado pelo sistema |
| `trainingId` | identificador | definido somente na rota; identifica o treinamento da inscrição |
| `firstName` | texto | obrigatório e não vazio |
| `lastName` | texto | obrigatório e não vazio |
| `email` | texto | obrigatório; espaços externos removidos antes do armazenamento e da comparação |

O e-mail será comparado sem diferença entre letras maiúsculas e minúsculas.

O mesmo e-mail poderá ser utilizado em treinamentos diferentes. Para um mesmo treinamento, após a normalização definida acima, cada e-mail poderá existir em apenas uma inscrição.

## Contrato da API para cadastro

### Requisição

- Método e rota: `POST /api/trainings/{trainingId}/attendees`
- `trainingId`: identificador do treinamento informado somente na rota.
- Corpo:

```json
{
  "firstName": "Ana",
  "lastName": "Silva",
  "email": "ana.silva@example.com"
}
```

O corpo não deve exigir nem aceitar `trainingId` como parte necessária do cadastro.

### Sucesso

Dados válidos para um treinamento existente produzem:

- status `201 Created`;
- identificador gerado para o inscrito;
- representação do inscrito criado;
- e-mail retornado sem espaços externos;
- localização do recurso criado.

Exemplo de resposta:

```json
{
  "id": "identificador-gerado",
  "trainingId": "identificador-do-treinamento",
  "firstName": "Ana",
  "lastName": "Silva",
  "email": "ana.silva@example.com"
}
```

### Falha de validação

Quando `firstName`, `lastName` ou `email` estiver ausente ou inválido, a API produz:

- status `400 Bad Request`;
- corpo no formato:

```json
{
  "errors": {
    "fieldName": ["Mensagem útil para correção."]
  }
}
```

A resposta identifica cada campo inválido.

### Treinamento inexistente

Quando não existir um treinamento correspondente ao `trainingId` informado na rota, a API produz:

- status `404 Not Found`;
- nenhuma inscrição é criada.

### E-mail repetido no mesmo treinamento

Quando já existir uma inscrição para o mesmo treinamento com o mesmo e-mail após a normalização, a API produz:

- status `409 Conflict`;
- corpo identificando o campo `email`:

```json
{
  "errors": {
    "email": ["Este e-mail já está inscrito neste treinamento."]
  }
}
```

A segunda inscrição não é armazenada.

O mesmo e-mail enviado para outro treinamento válido deve ser aceito.

## Contrato da API para listagem

### Requisição

- Método e rota: `GET /api/trainings/{trainingId}/attendees`
- `trainingId` é informado somente na rota.

### Sucesso

Quando o treinamento existir, a API produz:

- status `200 OK`;
- uma coleção de inscritos vinculados ao treinamento;
- somente dados dos inscritos daquele treinamento.

Exemplo de resposta:

```json
[
  {
    "id": "identificador-gerado",
    "trainingId": "identificador-do-treinamento",
    "firstName": "Ana",
    "lastName": "Silva",
    "email": "ana.silva@example.com"
  }
]
```

Quando o treinamento existir, mas não possuir inscritos, a API retorna `200 OK` com uma coleção vazia.

A listagem não deve incluir inscritos vinculados a outros treinamentos.

### Treinamento inexistente

Quando não existir um treinamento correspondente ao `trainingId` informado na rota, a API produz:

- status `404 Not Found`;
- nenhuma coleção de inscritos é retornada.

## Comportamento da interface

- disponibilizar o cadastro de um inscrito a partir da visualização de um treinamento;
- disponibilizar os campos `firstName`, `lastName` e `email`;
- não solicitar `trainingId` como campo editável;
- impedir ou proteger novo envio enquanto o cadastro estiver em andamento;
- informar sucesso somente depois da confirmação da API;
- atualizar a listagem do treinamento após um cadastro bem-sucedido;
- exibir os dados normalizados conforme retornados pela API;
- apresentar mensagem útil quando ocorrer erro;
- preservar os dados preenchidos quando o cadastro falhar;
- apresentar somente as operações de cadastro e listagem de inscritos.

## Critérios de aceitação

1. Dado um treinamento existente e um `firstName` ausente, quando o cadastro for enviado, então a API retorna `400` e identifica `firstName`.

2. Dado um treinamento existente e um `lastName` ausente, quando o cadastro for enviado, então a API retorna `400` e identifica `lastName`.

3. Dado um treinamento existente e um `email` ausente, quando o cadastro for enviado, então a API retorna `400` e identifica `email`.

4. Dado um treinamento inexistente, quando o cadastro for enviado para seu `trainingId`, então a API retorna `404` e nenhuma inscrição é criada.

5. Dado um treinamento existente e dados válidos, quando o cadastro for enviado, então a API retorna `201`, gera um identificador e retorna a representação do inscrito criado.

6. Dado um e-mail com espaços externos, quando o cadastro for enviado, então a inscrição armazenada e retornada usa o e-mail sem esses espaços.

7. Dado um treinamento existente com uma inscrição para `ana@example.com`, quando outra inscrição for enviada para o mesmo treinamento com `ANA@EXAMPLE.COM`, então a API retorna `409`, identifica `email` e não armazena uma segunda inscrição.

8. Dado um treinamento existente com uma inscrição para determinado e-mail, quando uma inscrição com o mesmo e-mail for enviada para outro treinamento existente, então a API aceita o cadastro.

9. Dado um treinamento existente com inscritos cadastrados, quando a listagem for consultada, então a API retorna `200` e somente os inscritos daquele treinamento.

10. Dado um treinamento existente sem inscritos, quando a listagem for consultada, então a API retorna `200` com uma coleção vazia.

11. Dado um treinamento inexistente, quando a listagem for consultada, então a API retorna `404`.

12. Pela interface, dados válidos produzem confirmação após a resposta bem-sucedida da API e fazem o novo inscrito aparecer na lista.

13. Pela interface, uma falha no cadastro preserva os dados preenchidos e apresenta uma mensagem útil.

## Evidências esperadas

| Critério | Evidência mínima |
| --- | --- |
| validação de `firstName` | resposta HTTP `400` e teste automatizado |
| validação de `lastName` | resposta HTTP `400` e teste automatizado |
| validação de `email` | resposta HTTP `400` e teste automatizado |
| treinamento inexistente no cadastro | resposta HTTP `404` e teste automatizado |
| cadastro válido | resposta HTTP `201`, identificador e teste automatizado |
| normalização do e-mail | teste automatizado confirmando remoção dos espaços externos |
| unicidade no treinamento | resposta HTTP `409`, erro em `email` e teste confirmando que a segunda inscrição não foi armazenada |
| mesmo e-mail em treinamentos diferentes | teste automatizado confirmando dois cadastros válidos |
| listagem com inscritos | resposta HTTP `200` e teste confirmando os itens do treinamento |
| listagem vazia | resposta HTTP `200` com coleção vazia e teste automatizado |
| treinamento inexistente na listagem | resposta HTTP `404` e teste automatizado |
| armazenamento | consulta bem-sucedida depois de reiniciar a API |
| sucesso na interface | fluxo de cadastro e listagem executado no navegador |
| erro na interface | fluxo de falha executado no navegador, confirmando preservação dos dados |
| integração contínua | workflow executando build e testes |

## Decisões resolvidas

- **Formato dos identificadores:** os campos `id` e `trainingId` devem usar o formato `Guid`.
- **Validade do e-mail:** um e-mail é válido quando possui o caractere `@` e, no mínimo, 7 caracteres.
- **Nomes com espaços externos:** `firstName` e `lastName` devem ter espaços externos removidos antes de armazenar e retornar.
- **Limites dos campos:** não há limites mínimo ou máximo adicionais para `firstName`, `lastName` e `email` além das regras já definidas no contrato.
- **`trainingId` malformado:** quando o valor estiver malformado, a API deve retornar `404 Not Found`.
- **Ordem da listagem:** a coleção de inscritos deve ser retornada em ordem determinística decrescente.
- **Localização do recurso criado e fluxo da interface:** no sucesso do cadastro, a API mantém o cabeçalho `Location` conforme o contrato HTTP do recurso criado, e a interface retorna para a tela de cadastro de aluno após a confirmação de sucesso.
- **Mensagens de erro:** não precisam ser normativas (texto fixo), mas devem identificar corretamente os campos inválidos no objeto `errors`.
