# Passo 4 — Implementar API e persistência

**Tempo sugerido: 40 minutos**

1. Abra uma nova sessão no modo **Agent**.
2. Referencie a nova especificação, os contratos existentes, o `DbContext` e os testes.
3. Peça um plano curto com arquivos, contratos, modelo de dados e validações.
4. Revise especialmente a rota, a normalização do e-mail e a unicidade por treinamento.
5. Aprove o plano antes das edições.
6. Acompanhe a criação dos contratos, entidade, relacionamento e endpoints.
7. Peça testes pela API pública para os cenários principais e de erro.
8. Execute os testes direcionados antes de gerar a migration.
9. Gere a migration, mas não a aplique imediatamente.
10. Use a skill disponível para revisar relacionamento, chave estrangeira e índice.
11. Aplique a migration somente depois da revisão.
12. Execute novamente os testes e inspecione ao menos uma resposta HTTP.

A implementação deve atravessar somente as camadas necessárias para:

- cadastrar um inscrito em um treinamento;
- listar os inscritos desse treinamento;
- persistir os dados com Entity Framework Core e SQLite;
- impedir no banco e na API que o mesmo e-mail seja inscrito duas vezes no mesmo treinamento;
- preservar os contratos existentes de treinamentos;
- gerar e revisar uma migration antes de aplicá-la;
- comprovar os principais contratos com testes pela API pública.

Use a skill de revisão de migration já disponível no repositório. Confirme que a unicidade é
por treinamento, e não global, e que um treinamento inexistente não recebe inscrições.

> [!TIP]
> Para contratos sugeridos, prompt completo, cenários de teste e comandos, consulte as
> [instruções completas](https://github.com/{{ repository }}/blob/main/.github/help/04-implementar-api-persistencia.md).

Quando API, migration e testes direcionados estiverem concluídos, comente `persistido`.
