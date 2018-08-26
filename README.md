# CalculadoraFreelancer04

Se trata da continuação do app <a href="https://github.com/dayaneLima/CalculadoraFreelancer03">CalculadoraFreelancer03</a>

## Criação do projeto CalculadoraFreelancer.Domain

Vamos criar um projeto chamado CalculadoraFreelancer.Domain do tipo Class Library do .NET Standard. 
Uma Class Library significa que esse projeto não  será executado, somente adicionado em outros projetos.
Esse projeto será responsável por conter nossos domínios, ou seja, nossas entidades e assinaturas de repositório que veremos na próxima aula.

Clique com o botão direito sobre a Solution, vá em Add -> New Project e escolha .NET Standard -> Class Library(.NET Standard). 
Dê um nome para o projeto, chame de de CalculadoraFreelancer.Domain. Em Location, mude o diretório para criação do projeto, pois ele sempre escolhe a pasta raiz.

Clique em Browser e selecione a pasta com o nome do projeto chamado CalculadoraFreelancer. Após, clique em selecionar pasta. Por fim clique em ok.

Abaixo é mostrado esses passos:

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_criacao_projeto.png" alt="Criação projeto >NET Standard" width="100%">

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_criacao_projeto_02.png" alt="Criação projeto >NET Standard" width="100%">

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_criacao_projeto_03.png" alt="Criação projeto >NET Standard" width="100%">

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_criacao_projeto_04.png" alt="Criação projeto >NET Standard" width="100%">

Gif com o processo completo:

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Gifs/CalculadoraFreelancer.Domain.gif" alt="Criação projeto >NET Standard" width="100%">

É sempre criado uma classe chamada Class1.cs, vamos a excluir pois não será utilizada. A exclusão não tem segredo, clique sobre o arquivo e aperte delete ou clique com o botão direito e clique em delete.

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_exclusao_classe.png" alt="Criação projeto >NET Standard" width="100%">


## Criação do projeto CalculadoraFreelancer.Domain.Core

Vamos repetir o processo anterior, mas agora vamos chamar de CalculadoraFreelancer.Domain.Core. Não se esqueça de alterar o Location.
Esse projeto é responsável por manter as entidades básicas para serem utilizadas no projeto Domain, facilitando seu reuso.

## Criação do projeto CalculadoraFreelancer.Infra.Data

Vamos repetir o processo anterior novamente, mas agora vamos chamar de CalculadoraFreelancer.Infra.Data. Não se esqueça novamente de alterar o Location. Essa camada será responsável pelo acesso a dados, armazenamento e obtenção dos mesmos.

## Estrutura

Nossa estrutura do projeto ficou da seguinte forma:

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_estrutura_projeto.png" alt="Criação projeto >NET Standard" width="100%">

## Reestruturação dos arquivos

Vamos agora trocar de lugar algumas de nossas classes que estão no projeto principal, vamos distribuí-las nos projetos corretos.

### CalculadoraFreelancer.Domain.Core

Vamos criar dentro do projeto CalculadoraFreelancer.Domain.Core uma pasta chamada Models, dentro dela vamos criar uma classe chamada Entity.

Vamos pensar o que há em comum em todas as nossas entidades que podemos colocar na nossa classe Entity. Todas as nossas entidades tem a propriedade Id, CreatedAt, UpdatedAt e Version. Vamos então criar esses atributos nessa classe. Ficará assim:

```c#
   public class Entity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Version]
        public string Version { get; set; }
    }
````

Está ocorrendo um erro pois nesse projeto não há a biblioteca do Azure, vamos resolver isso a instalando. 
Clique com o botão direito sobre o projeto CalculadoraFreelancer.Domain.Core e vá em Manage Nuget Packages..., vá em Browse e pesquise por Microsoft.Azure.Mobile.Client, após encontrá-la clique em install.

Após a instalação, vamos na nossa classe Entity e dar o using nesse pacote (aperte ctrl + .  que o visual studio te sugere o using).

### CalculadoraFreelancer.Domain

Vamos criar no projeto CalculadoraFreelancer.Domain uma pasta chamada Profissionais, pois ela representará o nosso domínio relacionado ao profissional. 

Vamos agora arrastar a classe Profissional que está dentro de Models do projeto CalculadoraFreelancer para esta pasta que acabamos de criar. Ao arrastar o Visual Studio cria uma cópia, então apague a que ficou no projeto principal (Dentro de Models, no projeto CalculadoraFreelancer).

Agora nossa classe Profissional pode herdar da classe Entity, vamos então adicionar ao nosso projeto uma referência para o projeto CalculadoraFreelancer.Domain.Core. Dentro do projeto CalculadoraFreelancer.Domain, em Dependencies, clique com o botão direito e vá em Add reference, na tela que irá abrir, vá em Projects e marque o projeto CalculadoraFreelancer.Domain.Core e clique em Ok.

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_add_reference.png" alt="Criação projeto >NET Standard" width="100%">


<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_add_reference_02.png" alt="Criação projeto >NET Standard" width="100%">

Agora sim, vamos editar a nossa classe Profissional e herdar da classe Entity. Após, o próprio Visual Studio irá nos alertar que há propriedades repetidas entre a Profissional e a Entity, ele faz isso as sublinhando de verde. Vamos remover da classe Profissional as propriedades Id, CreatedAt, UpdatedAt e Version. A classe ficará assim:

```c#
 [DataTable("Profissional")]
    public class Profissional : Entity
    {
        public double ValorGanhoMes { get; set; }
        public int HorasTrabalhadasPorDia { get; set; }
        public int DiasTrabalhadosPorMes { get; set; }
        public int DiasFeriasPorAno { get; set; }
        public int DiasDoencaPorAno { get; set; }
        public double ValorPorHora { get; set; }
    }
````

Vamos repetir o mesmo processo para a classe Projeto, no projeto CalculadoraFreelancer.Domain crie uma pasta chamada Projetos, após, arraste a model Projeto do projeto principal para esta pasta criada. Exclua a pasta Models do projeto principal.

A classe Projeto deve herdar da classe Entity, e excluir os atributos repetidos. A classe ficará assim:

```c#
  [DataTable("Projeto")]
    public class Projeto : Entity
    {
        public string Nome { get; set; }
        public double ValorPorHora { get; set; }
        public int HorasPorDia { get; set; }
        public int DiasDuracaoProjeto { get; set; }
        public double ValorTotal { get; set; }
    }
````

### CalculadoraFreelancer.Infra.Data

Vamos arrastar a pasta Repository do projeto principal para o projeto CalculadoraFreelancer.Infra.Data e após vamos apagar esta pasta do projeto principal.

Vamos adicionar a referência do projeto CalculadoraFreelancer.Domain, vá em Dependencies, clique com o botão direito e vá em Add Reference, escolha Projects e marque o projeto CalculadoraFreelancer.Domain e clique em Ok.

### CalculadoraFreelancer

Agora vamos no projeto principal para adicionar a referência para os projetos CalculadoraFreelancer.Domain e CalculadoraFreelancer.Infra.Data. Vá em Dependencies, clique com o botão direito e vá em Add Reference, escolha Projects e marque os dois projetos ( CalculadoraFreelancer.Domain e CalculadoraFreelancer.Infra.Data) e clique em Ok.

Vamos agora criar uma pasta chamada Views no projeto CalculadoraFreelancer, após, vamos arratas as três views que temos atualmente no projeto, a CalculoValorHoraPage.xaml, MainPage.xaml e ProjetoPage.xaml.

## Resultado

Reestruturamos nosso projeto, ficando melhor dividido. A estrutura de pastas final ficou assim:

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_estrutura_pasta_final.png" alt="Criação projeto >NET Standard" width="260">

## Converters em Xamarin Forms

No Xamarin Forms há os conhecidos Converter que são bastante utilizados em conjunto com o MVVM. 

Quando temos valores de moeda, na nossa tela desejamos que esse valor apareça bonito para o nosso usuário, com o R$, casas decimais, tudo correto, mas se estamos utilizando o MVVM, esse valor de moeda na nossa ViewModel é do tipo double (ou algum tipo relacionado, como o float). Então como exibir um valor na tela para o usuário e ao mesmo tempo esse valor corresponder a tipagem da variável na ViewModel, ou seja, exibir a mesma informação, mas formatada diferente? Através dos Converters!!

Para criar um converter você irá implementar de IValueConverter, que te obrigará a implementar os métodos Convert e ConvertBack. A funçãoa Convert irá entregar o valor que será exibido na View, Já a função ConvertBack entregará o valor que será atribuido a variável Bindable na ViewModel.

###  Criando o Converter para Moeda

Vamos colocar os converters em prática criando um para moeda. 

Crie uma pasta no projeto principal chamada Converters. Após, crie uma classe chamada de MoedaConverter:

```c#
   public class MoedaConverter : IValueConverter
    {
       
    }
````

Ele dará um erro obrigando a implementar as funções Convert e ConvertBack.

```c#
   public class MoedaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
````

No Converter queremos receber um valor e convertê-lo para uma string no formato de moeda, pois é o que queremos exibir na nossa View. Ficará assim:

```c#
 public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
      return Decimal.Parse(value.ToString()).ToString("C");
  }
````

Agora queremos obter o valor que está na tela e convertê-lo para double, para que nossa ViewModel tenha o valor correto em sua variável Bindable. Vamos alterar a função ConvertBack, ela ficará assim:

```c#
 public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
      string valueFromString = Regex.Replace(value.ToString(), @"\D", "");

      if (valueFromString.Length <= 0)
          return 0m;

      long valueLong;
      if (!long.TryParse(valueFromString, out valueLong))
          return 0m;

      if (valueLong <= 0)
          return 0m;

      return valueLong / 100m;
  }
````

A classe completa ficará assim:

```c#
    public class MoedaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Decimal.Parse(value.ToString()).ToString("C");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueFromString = Regex.Replace(value.ToString(), @"\D", "");

            if (valueFromString.Length <= 0)
                return 0m;

            long valueLong;
            if (!long.TryParse(valueFromString, out valueLong))
                return 0m;

            if (valueLong <= 0)
                return 0m;

            return valueLong / 100m;
        }
    }
````

### Adicionando o Converter nos nossos xaml

Vamos editar o arquivo chamado ProjetoPage.xaml. Primeiramente vamos adicionar uma referência para nossos converters, então na tag do ContentPage, adicione o seguinte trecho de código:

```xml
 xmlns:converters="clr-namespace:CalculadoraFreelancer.Converters"
````

Ficará assim:

```xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalculadoraFreelancer.Views.ProjetoPage"
             xmlns:converters="clr-namespace:CalculadoraFreelancer.Converters"
             Icon="money.png"
             Padding="10">
````

Agora temos que criar uma Key para acessar esse Converter, para isso precisamos declará-lo no ResourceDictionary da nossa tela, dessa forma:

```xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalculadoraFreelancer.Views.ProjetoPage"
             xmlns:converters="clr-namespace:CalculadoraFreelancer.Converters"
             Icon="money.png"
             Padding="10">
   
       <ContentPage.Resources>
           <ResourceDictionary>
               <converters:MoedaConverter x:Key="MoedaConverter" />
           </ResourceDictionary>
       </ContentPage.Resources>
   
   ...
````

Agora no Text do Entry de valor por hora vamos chamar nosso Converter, dessa forma:

```xml
       <Entry Placeholder="Valor por hora"
                   Keyboard="Numeric"
                   Text="{Binding ValorPorHora, Converter={StaticResource MoedaConverter}}"/>
````

Nossa tela completa ficou dessa forma:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalculadoraFreelancer01.ProjetoPage"
             Title="Projeto"
             Padding="10">
    <ContentPage.Content>
        <StackLayout>

            <Label Text="Nome do Projeto" />
            <Entry Placeholder="Nome do projeto"
                   Text="{Binding Nome}" />

            <Label Text="Valor por hora" />
            <Entry Placeholder="Valor por hora"
                   Keyboard="Numeric"
                   Text="{Binding ValorPorHora, Converter={StaticResource MoedaConverter}}"/>

            <Label Text="Horas por dia" />
            <Entry Placeholder="Horas por dia"
                   Keyboard="Numeric"
                   Text="{Binding HorasPorDia}"/>

            <Label Text="Dias de duração do projeto" />
            <Entry Placeholder="Dias de duração do projeto"
                   Keyboard="Numeric"
                   Text="{Binding DiasDuracaoProjeto}"/>

            <Label FontSize="Large"
                   Text="{Binding ValorTotal, StringFormat='Total: {0:C}'}"/>

            <Grid>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="*" />
                    <ColumnDefinition Width="*" />
                </Grid.ColumnDefinitions>

                <Button Grid.Column="0"
                        BackgroundColor="#cdcdcd"
                        Text="Limpar"
                        Command="{Binding LimparCommand}"/>

                <Button Grid.Column="1"
                        Text="Gravar"
                        TextColor="White"
                        BackgroundColor="#6699ff"
                        Command="{Binding GravarCommand}"/>

            </Grid>

        </StackLayout>
    </ContentPage.Content>
</ContentPage>
````
