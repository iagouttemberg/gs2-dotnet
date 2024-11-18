# API EcoTrack - Microservices

## Visão Geral do Projeto

O **EcoTrack** é uma plataforma inovadora de monitoramento e análise de consumo de energia, criada
para ajudar os usuários a entenderem e gerenciarem seu uso de energia de maneira mais consciente e
sustentável. O aplicativo EcoTrack permite que os usuários registrem manualmente seu consumo
energético, visualizem uma análise gerada por IA sobre seu perfil de consumo e tenham acesso a dicas
práticas de economia de energia. A aplicação é intuitiva, centrada no usuário e utiliza tecnologias
avançadas para criar uma experiência integrada e eficiente.

## Objetivos do Projeto

1. **Monitoramento e Registro de Consumo de Energia:** Permitir que os usuários registrem manualmente seu consumo mensal de energia e acompanhem o histórico ao longo do tempo.

2. **Análise de Consumo Baseada em IA:** Utilizar uma inteligência artificial generativa para classificar o consumo mensal em alto, médio ou baixo, fornecendo feedback personalizado com base nos dados históricos do usuário.

3. **Dicas de Economia de Energia:** Proporcionar dicas e orientações sustentáveis que ajudam os usuários a adotarem práticas de economia de energia no dia a dia.


## Arquitetura da API: Microservices

### Por que escolhemos microservices?

1. **Escalabilidade Independente**  
   Cada microserviço pode ser escalado individualmente. Por exemplo, o serviço de **Usuario** pode ter maior demanda e, portanto, ser escalado sem a necessidade de aumentar a infraestrutura do serviço de **DicaEconomia**.

2. **Desenvolvimento Modular**  
   Cada microserviço é uma aplicação isolada, permitindo que diferentes equipes trabalhem em paralelo nos serviços de **Usuario**, **ConsumoEnergetico** e **DicaEconomia**. Isso facilita a implantação e a manutenção, uma vez que o deploy de um microserviço não interfere no outro.

3. **Facilidade de Manutenção**  
   Com uma aplicação modular, fica mais simples testar, atualizar e corrigir erros em uma parte específica do sistema, sem impactar o restante.

4. **Isolamento de Falhas**  
   Se um serviço falha (por exemplo, o serviço de **ConsumoEnergia**), ele não compromete o funcionamento dos demais serviços, como o de **Usuario**, garantindo maior robustez do sistema como um todo.

5. **Tecnologias e Escalabilidade Independente**  
   É possível escolher tecnologias e bancos de dados diferentes para cada serviço, caso necessário. Por exemplo, o serviço de **DicaEconomia** pode usar uma tecnologia ou banco de dados otimizado para dados não-relacionais, enquanto o de **Usuario** pode permanecer com uma solução relacional.

## Testes Implementados

### Serviço externo
Esse teste é uma unidade de teste para o serviço de envio de e-mails. O objetivo principal do teste é verificar o comportamento do método `SendEmailAsync` quando uma chave API inválida é fornecida. Esse teste é uma boa prática para garantir que seu serviço de e-mail não tente enviar e-mails com uma configuração inválida, o que poderia resultar em falhas em produção. A abordagem de testes unitários ajuda a garantir que cada parte do código se comporte conforme esperado em diferentes cenários.

### Repositório
Esse teste é uma unidade de teste para a classe `UsuarioRepository`, que utiliza um banco de dados em memória para verificar a funcionalidade de adição de uma entidade `Usuario`. Esse teste é uma boa prática para garantir que a funcionalidade de adição de usuários no repositório funcione corretamente. Ele assegura que, ao adicionar um usuário, a operação de inserção se comporta como esperado e que os dados são armazenados corretamente no banco de dados. O uso de um banco de dados em memória para testes é eficiente, pois não requer configuração de um banco de dados real e facilita a execução de testes isolados.

---

## Instruções para rodar a API:

1. Fazer o **Download** do arquivo `.zip` no GitHub.
2. **Extrair** o projeto do arquivo `.zip`.
3. Abrir o **Visual Studio** e em seguida clicar em "abrir um projeto ou uma solução".
4. Abrir o projeto e clicar no arquivo `.sln`.
5. Apertar a tecla **F5** do teclado para executar o projeto.