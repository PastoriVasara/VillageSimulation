var menuIsOpen = false;

document.getElementById("submitButton").addEventListener("click", function () {
    convertToObject();
});
document.getElementById("addNewLevel").addEventListener("click", function () {
    addNewSubgroup("Subgroup","Value");
});
$(document).ready(function () {
    loadFromFile();
});
function loadFromFile() {
    title = document.title;

    $.getJSON(title + ".json", function (json) {
        //console.log(json);
        var desiredLevel = 1;
        for(var key in json)
        {

            //children, level, level_name
            
            var parent = addNewSubgroup(key,"value");
            console.log(parent);
            var parentDiv = document.getElementById(parent);
            for(var i = 0; i < json[key].children.length; i++)
            {
                desiredLevel++;
                var value = "";
                var child = json[key].children[i];
                for (childKey in child)
                {
                    console.log(child[childKey]);
                    if(Array.isArray(child[childKey])){
                    for(var j = 0; j < child[childKey].length; j++)
                    {
                        value += child[childKey][j];
                        if( j < child[childKey].length-1)
                        {
                            value += ";";
                        }
                        
                    }
                }
                else
                {
                    value = child[childKey];
                }
                }
                console.log(desiredLevel,childKey,value);
                parentDiv.appendChild(informationBlock("_"+"1_"+desiredLevel,childKey,value));
            }
        }
    });
}

function addNewSubgroup(name,value) {

    var query = "level_0"
    var highestMember = 0;
    var matches = document.querySelectorAll('[id^="' + query + '"]');
    for (var i = 0; i < matches.length; i++) {
        var currentNumber = matches[i].id.split("_")[2];
        if (currentNumber > highestMember) {
            highestMember = currentNumber;
        }

    }
    highestMember++;
    var parent = document.getElementById("jsonContainer");
    var desiredLevel = "_0_" + highestMember;
    console.log(name);
    parent.insertBefore(informationBlock(desiredLevel,name,value),document.getElementById("addLevelContainer"));
    return "children"+desiredLevel;
}

function buildList(children,level,level_name) {
    //recursiveBuild(object,document.getElementById("level_0_1"),1);
}

function addNewLevel(id,name,value) {
    console.log(id);
    var idSplit = id.split("_");
    var level = idSplit[1];
    var query = "level" + "_" + (parseInt(idSplit[1]) + 1);

    var textField = document.getElementById("fieldValue_" + idSplit[1] + "_" + idSplit[2]);
    if (textField != null) {
        textField.remove();
    }
    var highestMember = 0;
    console.log(query);
    var matches = document.querySelectorAll('[id^="' + query + '"]');
    console.log(matches);
    for (var i = 0; i < matches.length; i++) {
        var currentNumber = matches[i].id.split("_")[2];
        if (currentNumber > highestMember) {
            highestMember = currentNumber;
        }

    }
    highestMember++;
    var parent = document.getElementById("children_" + idSplit[1] + "_" + idSplit[2]);
    var desiredLevel = "_" + (parseInt(idSplit[1]) + 1) + "_" + highestMember;
    console.log(parent);
    parent.appendChild(informationBlock(desiredLevel,name,value));

}
function collapseLevel(id) {
    var idSplit = id.split("_");
    var level = "children_" + idSplit[1] + "_" + idSplit[2];

    var parent = document.getElementById(level);
    parent.style.display = getComputedStyle(parent, null).display === "block" ? "none" : "block"; 0
}

function openMenu(id) {
    menuIsOpen = true;
    var menu = document.getElementById("menu");
    menu.style.display = "block";
    var editText = document.getElementById("editValue");
    editText.value = document.getElementById(id).innerHTML;
    var newButton = document.createElement("button");
    newButton.id = "button_" + id;
    newButton.className = "hiddenBox";
    newButton.innerHTML = "Close";
    document.getElementById("hiddenLayer").appendChild(newButton);
    newButton.addEventListener("click", function () {
        var finishedText = document.getElementById("editValue").value;
        var editID = this.id.split("_");
        var gottenID = "";
        for (var i = 1; i < editID.length; i++) {
            if (i != 1) {
                gottenID += "_" + editID[i];
            }
            else {
                gottenID = editID[i];
            }
        }
        var editableValue = document.getElementById(gottenID);
        editableValue.innerHTML = finishedText;
        closeMenu(this.id);
    });

}
function closeMenu(id) {

    var menu = document.getElementById("menu");
    menu.style.display = "none";
    menuIsOpen = false;
    document.getElementById(id).remove();
}
function removeLevel(id) {
    var idSplit = id.split("_");
    var level = "level_" + idSplit[1] + "_" + idSplit[2];
    var parent = document.getElementById(level);
    console.log(idSplit[1]);
    if (idSplit[1] == "0") {
        children = "children_" + idSplit[1] + "_" + idSplit[2];
        var childDiv = document.getElementById(children);
        childDiv.remove();
        var newChild = document.createElement("div");
        newChild.id = children;
        parent.appendChild(newChild);
    }
    else {

        var first = parent.firstElementChild;
        while (first) {
            first.remove();
            first = parent.firstElementChild;
        }
        parent.remove();
    }


}

function informationBlock(id, name,value) {
    console.log(id);
    var headLevel = document.createElement("div");
    headLevel.className = "jsonLevel";
    headLevel.id = "level" + id;
    var blockHead = document.createElement("div");
    blockHead.className = "blockHeader";

    var buttonContainer = document.createElement("div");
    buttonContainer.className = "jsonButtons";
    var level = id.split("_")[1];
    //TODO: Fix recursive with a smart way

    if (level == "0") {
        var collapse = document.createElement("i");
        collapse.className = "fa fa-compress fa-2x";
        collapse.id = "collapseLevel" + id;
        collapse.addEventListener("click", function () {
            collapseLevel(collapse.id);
        });

        var add = document.createElement("i");
        add.className = "fa fa-plus fa-2x";
        add.id = "addLevel" + id;
        add.addEventListener("click", function () {
            addNewLevel(add.id,"Group-Type","value");
        });



        buttonContainer.appendChild(collapse);
        buttonContainer.appendChild(add);
    }
    var remove = document.createElement("i");
    remove.className = "fa fa-minus fa-2x";
    remove.id = "removeLevel" + id;
    remove.addEventListener("click", function () {
        removeLevel(remove.id);
    });
    buttonContainer.appendChild(remove);

    blockHead.appendChild(buttonContainer);

    var singleField = document.createElement("div");
    singleField.className = "singleField";

    var fieldName = document.createElement("h1");
    fieldName.contentEditable = true;
    fieldName.id = "fieldName" + id;
    fieldName.innerHTML = name;
    singleField.appendChild(fieldName);
    if (level != "0") {
        var fieldValue = document.createElement("div");
        fieldValue.id = "fieldValue" + id;
        fieldValue.className = "fieldClass";
        fieldValue.innerHTML = value;
        fieldValue.addEventListener("click", function () {
            if(!menuIsOpen){
            openMenu(this.id);
            }
        });
        singleField.appendChild(fieldValue);
    }



    blockHead.appendChild(singleField);

    headLevel.appendChild(blockHead);
    var childrens = document.createElement("div");
    childrens.id = "children" + id;
    childrens.className = "childrenContainer";
    headLevel.appendChild(childrens);

    return headLevel;
}

function convertToObject() {
    var finalObject = {};
    var finished = false;
    var root = document.getElementById("jsonContainer");
    var query = "level_0";
    var matches = document.querySelectorAll('[id^="' + query + '"]');
    var uniqueFields = [];
    var ableToContinue = true;
    for( var j = 1; j < matches.length+1; j++)
    {
        var currentField = document.getElementById("fieldName_0_"+j).innerHTML;
        if(uniqueFields.includes(currentField))
        {
            ableToContinue = false;
            alert("ERROR! Identical Group Names, please change them.");

        }
        else
        {
            uniqueFields.push(currentField);
        }
    }
    if(ableToContinue){
    for(var i = 0; i < matches.length; i++)
    {
        var id = matches[i].id;
        console.log(id);
        var splitID = id.split("_");
        var fieldName = "fieldName_"+splitID[1]+"_"+splitID[2];
        console.log(fieldName);
        var objectName = document.getElementById("fieldName_"+splitID[1]+"_"+splitID[2]).innerHTML;
        var parent = document.getElementById(id);
        var childs = document.getElementById("children_0_"+splitID[2]);
        console.log("children_0_"+splitID[2]);
        console.log(childs);
        finalObject[objectName] = {};
        finalObject[objectName]["children"] = [];
        testing = recursiveGoThrough(childs.childNodes,finalObject,objectName);
    }
    console.log(JSON.stringify(testing));
    postResults(testing);
}
}

function postResults(object) {
    title = document.title;
    var url = "http://localhost:3001/" + title;
    var xhr = new XMLHttpRequest();
    xhr.open("POST", url, true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(object));
}
function recursiveGoThrough(children, object, parent) {
    for (var i = 0; i < children.length; i++) {
        if (children[i].tagName == 'DIV') {
            var childID = children[i].id.split("_");
            var level = childID[1];
            var iterationOfLevel = childID[2];
            var propertyName = document.getElementById("fieldName_" + level + "_" + iterationOfLevel).innerHTML;
            console.log(propertyName);
            var propertyValue = document.getElementById("fieldValue_" + level + "_" + iterationOfLevel);

            if (propertyValue != null) {
                console.log(propertyValue);
                propertyValue = propertyValue.innerHTML.split(";");
                propertyValue = propertyValue.length == 1 ? propertyValue[0] : propertyValue;
            }
            var childContainer = document.getElementById("children_" + level + "_" + iterationOfLevel);
            if (childContainer.childElementCount > 0) {
                newLevel = {};
                newLevel[propertyName] = {};
                newLevel[propertyName]["children"] = [];
                var newObject = {};
                newObject[propertyName] = propertyValue;
                object[parent]["children"].push(newObject);
                recursed = recursiveGoThrough(childContainer.childNodes, newLevel, propertyName);

                object[parent]["children"].push(recursed);
            }
            else {
                var newObject = {};
                newObject[propertyName] = propertyValue;
                object[parent]["children"].push(newObject);
            }
        }
        else {
            console.log("not div!");
        }

    }
    return object;
}