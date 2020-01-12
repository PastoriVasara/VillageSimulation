
document.getElementById("addLevel_0_1").addEventListener("click" ,function()
{
    addNewLevel(this.id);
});
document.getElementById("removeLevel_0_1").addEventListener("click" ,function()
{
    removeLevel(this.id);
});
document.getElementById("collapseLevel_0_1").addEventListener("click" ,function()
{
    collapseLevel(this.id);
});

function addNewLevel(id)
{
    var idSplit = id.split("_");
    var level = idSplit[1];
    var query = idSplit[0] + "_" + (parseInt(idSplit[1])+1);
    console.log(query);
    var highestMember = 1;
    var matches = document.querySelectorAll('[id^="'+query+'"]');
    for(var i = 0; i < matches.length; i++)
    {
        console.log(matches[i].id);
        var currentNumber = matches[i].id.split("_")[2];
        if(currentNumber > highestMember)
        {
            console.log("this is higher!");
            highestMember = currentNumber;
        }

    }
    highestMember++;
    var parent = document.getElementById("children_"+idSplit[1]+"_"+idSplit[2]);
    var desiredLevel = "_" +  (parseInt(idSplit[1])+1) + "_" + highestMember;
    parent.appendChild(informationBlock(desiredLevel));
    
}
function collapseLevel(id)
{
    var idSplit = id.split("_");
    var level =  "children_" +idSplit[1] + "_"+idSplit[2];

    var parent = document.getElementById(level);
    console.log(getComputedStyle(parent, null).display === "block");
    parent.style.display = getComputedStyle(parent, null).display === "block" ? "none" : "block";

    /*
    for(var i = 0; i < children.length; i++)
    {
        if(children[i].id != null)
        {
            var childIdSplit = children[i].id.split("_");
            var childDepth = parseInt(childIdSplit[1]);
            if(childDepth>depth)
            {
                children[i].style.display = children[i].style.display === "flex" ? "none" : "flex";
            }
        }
        else{
        children[i].style.display = children[i].style.display === "flex" ? "none" : "flex";
        }
    }
    */
}

function openMenu(id)
{
    var menu =document.getElementById("menu");
    menu.style.display = "block";
    var editText = document.getElementById("editValue");
    editText.value= document.getElementById(id).innerHTML;
    var newButton = document.createElement("button");
    newButton.id = "button_"+id;
    newButton.className = "hiddenBox";
    newButton.innerHTML = "Close";
    document.getElementById("hiddenLayer").appendChild(newButton);
    newButton.addEventListener("click",function(){
        var finishedText = document.getElementById("editValue").value;
        var editID = this.id.split("_");
        var gottenID = "";
        for(var i = 1; i < editID.length; i++)
        {
            if(i != 1){
            gottenID += "_" +editID[i];
            }
            else
            {
                gottenID = editID[i];
            }
        }
        console.log(gottenID);
        var editableValue = document.getElementById(gottenID);
        editableValue.innerHTML = finishedText;
        closeMenu(this.id);
    });
    
}
function closeMenu(id)
{
    var menu =document.getElementById("menu");
    menu.style.display = "none";

    document.getElementById(id).remove();
}
function removeLevel(id)
{
    var idSplit = id.split("_");
    var level =  "level_" +idSplit[1] + "_"+idSplit[2];
    var parent = document.getElementById(level);
    console.log(idSplit[1]);
    if(idSplit[1] == "0"){
        console.log("true");
        children = "children_" +idSplit[1] + "_"+idSplit[2];
        var childDiv = document.getElementById(children);
        childDiv.remove();
        var newChild = document.createElement("div");
        newChild.id = children;
        parent.appendChild(newChild);
    }
    else{
        
        var first = parent.firstElementChild;
    while(first)
    {
        first.remove();
        first = parent.firstElementChild;
    }
    parent.remove();
    }
    

}

function informationBlock(id)
{
    /*
    <div id="level_1_1" class="jsonLevel">
                <div class="blockHeader">
                    <div class="jsonButtons">
                        <i id="collapseLevel_1_1" class="fa fa-compress fa-2x"></i>  
                        <i id="addLevel_1_1" class="fa fa-plus fa-2x"></i>
                        <i id="removeLevel_1_1" class="fa fa-minus fa-2x"></i>
                    </div>
                    <div class="singleField">
                    <h1 id="fieldName_1_1" contenteditable="true">Name</h1>
                    <div contenteditable="true"> Value</div>
                </div>
                </div>
            </div>
            */
    var headLevel = document.createElement("div");
    headLevel.className = "jsonLevel";
    headLevel.id = "level"+id;
    var blockHead = document.createElement("div");
    blockHead.className = "blockHeader";

    var buttonContainer = document.createElement("div");
    buttonContainer.className = "jsonButtons";

    var collapse = document.createElement("i");
    collapse.className = "fa fa-compress fa-2x";
    collapse.id = "collapseLevel"+id;
    collapse.addEventListener("click", function(){
        collapseLevel(collapse.id);
    });

    var add = document.createElement("i");
    add.className = "fa fa-plus fa-2x";
    add.id = "addLevel"+id;
    add.addEventListener("click", function(){
        addNewLevel(add.id);
    });

    var remove = document.createElement("i");
    remove.className = "fa fa-minus fa-2x";
    remove.id = "removeLevel"+id;
    remove.addEventListener("click", function(){
        removeLevel(remove.id);
    });

    buttonContainer.appendChild(collapse);
    buttonContainer.appendChild(add);
    buttonContainer.appendChild(remove);

    blockHead.appendChild(buttonContainer);

    var singleField = document.createElement("div");
    singleField.className="singleField";

    var fieldName = document.createElement("h1");
    fieldName.contentEditable = true;
    fieldName.id="fieldName"+id;
    fieldName.innerHTML = "name";

    var fieldValue = document.createElement("div");
    fieldValue.id = "fieldValue"+id;
    fieldValue.className = "fieldClass";
    fieldValue.innerHTML = "value";
    fieldValue.addEventListener("click",function(){
        openMenu(this.id);
    });

    singleField.appendChild(fieldName);
    singleField.appendChild(fieldValue);

    blockHead.appendChild(singleField);

    headLevel.appendChild(blockHead);
    var childrens = document.createElement("div");
    childrens.id = "children"+id;
    childrens.className="childrenContainer";
    headLevel.appendChild(childrens);

    return headLevel;
}