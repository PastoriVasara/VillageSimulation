import urllib.request, json

nameList = []
surnameFile = open("surnames.txt","w")
while len(nameList) < 10000:
    with urllib.request.urlopen("https://namegenerator.com/last/random") as url:
        data = json.loads(url.read().decode())
        
        surnames = data["last_names"]
        for j in range (len(surnames)):
            containsName = False
            for i in range(len(nameList)):
                if nameList[i] == surnames[j]:
                    containsName = True
            if not containsName:
                nameList.append(surnames[j])
    print(len(nameList))

for i in range(len(nameList)):
    surnameFile.write(nameList[i] + "\n")

surnameFile.close()