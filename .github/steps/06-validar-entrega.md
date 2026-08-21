# Passo 6 — Validar a fatia completa

**Tempo sugerido: 10 minutos**

Antes de encerrar, relacione cada critério da especificação a uma evidência. Não use apenas a
aparência da interface como prova de que persistência e regras de negócio estão corretas.

1. Reabra os critérios de aceitação da especificação.
2. Para cada critério, indique uma evidência concreta.
3. Execute restore, build e toda a suíte de testes.
4. Inspecione a migration e o snapshot do modelo.
5. Repita no navegador o caminho principal e o erro de duplicidade.
6. Revise o diff final pela área **Source Control** do VS Code, procurando alterações fora do
   escopo. Como alternativa, use `git diff` no terminal.
7. Registre divergências que não puderem ser resolvidas no tempo do lab.

Confirme:

1. build e testes existentes continuam passando;
2. os novos testes exercitam a API pública;
3. a migration representa relacionamento e unicidade esperados;
4. o mesmo e-mail pode existir em treinamentos diferentes, mas não duas vezes no mesmo;
5. a interface é alcançada pela lista de treinamentos;
6. sucesso, lista vazia, treinamento inexistente e duplicidade têm comportamento útil;
7. API e Client continuam usando os contratos documentados.

Use **Source Control** no VS Code para selecionar e comparar cada arquivo do diff final. Como
alternativa, execute `git diff` no terminal. Registre qualquer divergência conhecida em vez
de escondê-la.

> [!TIP]
> Para comandos, matriz de evidências e roteiro final, consulte as
> [instruções completas](https://github.com/{{ repository }}/blob/main/.github/help/06-validar-entrega.md).

Quando as evidências estiverem reunidas, comente `validado`.
