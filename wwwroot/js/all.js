async function exportDatabase(name) {
    const content = Module.FS.readFile(`/${name}`);
    const file = new File([content], name);
    const exportUrl = URL.createObjectURL(file);

    const a = document.createElement("a");
    document.body.appendChild(a);
    a.href = exportUrl;
    a.download = name;
    a.target = "_self";
    a.click();
}

async function deleteDatabase() {
    window.indexedDB.deleteDatabase("SqliteStorage");
}

async function synchronizeFileWithIndexedDb(filename) {
    return new Promise((res, rej) => {
        const db = window.indexedDB.open("SqliteStorage", 1);
        db.onupgradeneeded = () => {
            db.result.createObjectStore("Files", {keypath: "id"});
        };

        db.onsuccess = () => {
            const req = db.result.transaction("Files", "readonly").objectStore("Files").get("file");
            req.onsuccess = () => {
                Module.FS_createDataFile("/", filename, req.result, true, true, true);
                res();
            };
        };

        let lastModifiedTime = new Date();
        setInterval(() => {
                const path = `/${filename}`;
                if (Module.FS.analyzePath(path).exists) {
                    const mtime = Module.FS.stat(path).mtime;
                    if (mtime.valueOf() !== lastModifiedTime.valueOf()) {
                        lastModifiedTime = mtime;
                        const data = Module.FS.readFile(path);
                        db.result.transaction("Files", "readwrite").objectStore("Files").put(data, "file");
                    }
                }
            },
            1000);
    });
}
