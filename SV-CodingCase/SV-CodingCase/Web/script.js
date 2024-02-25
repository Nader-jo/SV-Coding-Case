document.getElementById("searchForm").oninput = async function (event) {
    event.preventDefault();
    const searchInput = encodeURIComponent(
        document.getElementById("searchInput").value
    );
    const resultsContainer = document.getElementById("searchResults");
    
    if (searchInput === "") {
        resultsContainer.innerHTML = "";
        return;
    }
    const response = await fetch(
        `http://localhost:5031/api/v1/Search?searchInput=${searchInput}`
    );
    const data = await response.json();

    
    if (data.status === "success") {
        const { buildings, locks, groups, medium } = data.result;
        const allItems = [
            ...locks.map((item) => ({ ...item, type: "Lock" })),
            ...buildings.map((item) => ({ ...item, type: "Building" })),
            ...groups.map((item) => ({ ...item, type: "Group" })),
            ...medium.map((item) => ({ ...item, type: "Medium" })),
        ];
        if (allItems.length == 0) {
            resultsContainer.textContent = "No results found.";
        } else {
            resultsContainer.textContent = "";
        }
        allItems.sort((a, b) => b.weight - a.weight);

        const unifiedList = document.createElement("ul");
        allItems.forEach((item) => {
            const listItem = document.createElement("li");

            listItem.classList.add("list-item");

            const icon = document.createElement("img");
            icon.classList.add("icon");
            let iconPath = "";
            switch (item.type) {
                case "Lock":
                    iconPath = "./Resources/lock-icon.png";
                    break;
                case "Building":
                    iconPath = "./Resources/building-icon.png";
                    break;
                case "Group":
                    iconPath = "./Resources/group-icon.png";
                    break;
                case "Medium":
                    iconPath = "./Resources/medium-icon.png";
                    break;
            }
            icon.setAttribute("src", iconPath);
            let textContent = document.createElement("span");
            textContent.classList.add("item-text");
            if (item.type == "Lock") {
                textContent = `Id: ${item.lock.id}\nName: ${item.lock.name}\nBuilding Id: ${item.lock.buildingId}\nType: ${item.lock.type}\nSerial Number: ${item.lock.serialNumber}\nFloor: ${item.lock.floor}\nRoom Number: ${item.lock.roomNumber}\nDescription: ${item.lock.description}`;
            } else if (item.type == "Building") {
                textContent = `Id: ${item.building.id}\nName: ${item.building.name}\nShortCut: ${item.building.shortCut}\nDescription: ${item.building.description}`;
            } else if (item.type == "Group") {
                textContent = `Id: ${item.group.id}\nName: ${item.group.name}\nDescription: ${item.group.description}`;
            } else if (item.type == "Medium") {
                textContent = `Id: ${item.medium.id}\nGroup Id: ${item.medium.groupId}\nType: ${item.medium.type}\nSerial Number: ${item.medium.serialNumber}\nOwner: ${item.medium.owner}\nDescription: ${item.medium.description}`;
            }

            listItem.appendChild(icon);
            listItem.appendChild(document.createTextNode(textContent));
            unifiedList.appendChild(listItem);
            unifiedList.firstChild.style.backgroundColor = 'lightgreen';
        });

        resultsContainer.appendChild(unifiedList);
    } else {
        resultsContainer.textContent = "No results found.";
    }
};