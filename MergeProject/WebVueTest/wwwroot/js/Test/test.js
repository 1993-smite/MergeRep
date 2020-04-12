describe("replaceAll", function () {

    let words = ["Masasuka", "Message", "Tarakan"];
    let tryRes = ["Mssuk", "Massaga", "Tereken"];
    let argsReplace = [["a", ""], ["e", "a"], ["a", "e"]];

    for (let index in words) {
        it(`replace '${argsReplace[index][0]}' in word '${words[index]}'`, function () {
            assert.equal(
                words[index].replaceAll(argsReplace[index][0], argsReplace[index][1]), tryRes[index]);
        });
    }

});

describe("changeObjects", function () {

    let source = [{ test: 3 }, { rem: 2  }, {track:1}];
    let tryRes = [{ bor: 1  }, { brom: 2 }, {ftor: 3}];

    for (let index in source) {
        it(`changeObjects '${index}'`, function () {
            let obj1 = source[index];
            let obj2 = tryRes[index];
            [obj1, obj2] = changeObjects(obj1, obj2);
            assert.equal(obj1, tryRes[index]);
            assert.equal(obj2, source[index]);
        });
    }

});

describe("initObject", function () {

    let source = [["Alex", 9],["Roman", 4],["Petr", 5]];

    for (let index in source) {
        it(`initObject '${index}'`, function () {
            var obj = {};
            initObject.call(obj, source[index][0], source[index][1]);
            assert.equal(obj.name, source[index][0]);
            assert.equal(obj.value, source[index][1]);
        });
    }

});