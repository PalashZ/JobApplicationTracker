# JobApplicationTracker

## Project description
A console application written in C# that helps you keep track of your job applications. You can register new applications, update their status as the process moves forward, and get an overview of your whole job search through filtering, sorting and statistics.

<img width="568" height="200![Uploading CMD2.png…]()
" alt="CMD1" src="https://github.com/user-attachments/assets/20abe964-4f90-41b9-9f0e-a53a3c64fb5c" />
<img width="568" height="200" alt="image" src="https://github.com/user-attachments/assets/b5bf8f85-3752-4c73-b77e-804e0db4a69d" />

## Requirements
- .NET SDK (version 8 or later)
- Visual Studio Code, Visual Studio or any terminal
- Git Bash

### Option 1: Visual Studio and Visual Studio Code
1. Clone or download the repository from GitHub
2. Open the project folder "JobApplicationTracker":
- Visual Studio: Open the "JobApplicationTracker.slnx"
- Visual Studio Code: Choose the file and then "Open Folder", then can you select the project folder
4. Once you open the project folder in one of the programs, you can look at the codes
5. You can run the program
6. Choose an option that's in the program

### Option 2: Terminal 
1. Open the terminal in the project folder and select "JobApplicationTracker.csproj",
2. Run it
3. You can run the program
4. Choose an option that's in the program

============================================================================================

## Reflektionsfrågor (för inlämning)
1. Hur hjälpte LINQ dig att skriva renare kod?
- LINQ har underlättat för mig eftersom jag kan filtrera och sortera data på en enda rad. I stället för att skriva flera rader kod med loopar och if-satser räcker det med en rad, vilket gör koden kortare och lättare att läsa. som t.ex I ShowByStatus() använde jag till exempel Applications.Where(a => a.Status == selectedStatus).ToList(), Den här raden används för att få fram ansökningar med en viss status, allt på en enda rad.

2. Vilken del av projektet var mest utmanande, och hur löste du den?
- Det mest utmanande i projektet var JobManager-klassen och LINQ. JobManager innehåller mest logik i programmet: den ska läsa och kontrollera inmatningen, hantera listan med ansökningar och köra alla LINQ-frågor. Det var svårt att hålla ordning på alla delar, att kontrollera att inmatningen är giltig så att programmet inte kraschar. LINQ var nytt för mig, och lambda-uttryck som a => a.Status == selectedStatus var svåra att förstå i början. Jag löste det genom att bygga en metod i taget och testa den innan jag gick vidare, och genom att skriva kommentarer som förklarar vad varje rad gör. Uppgiften tog längre tid eftersom jag ville förstå exakt vad allt betyder. Jag tittade på videor, frågade AI hur man använder LINQ och gick igenom PowerPoint-presentationerna från lektionerna.

### Palash Zolfaghari 2026-10-18
