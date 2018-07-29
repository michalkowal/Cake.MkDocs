
var camelCaseTokenizer = function (obj) {
    var previous = '';
    return obj.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
        var current = cur.toLowerCase();
        if(acc.length === 0) {
            previous = current;
            return acc.concat(current);
        }
        previous = previous.concat(current);
        return acc.concat([current, previous]);
    }, []);
}
lunr.tokenizer.registerFunction(camelCaseTokenizer, 'camelCaseTokenizer')
var searchModule = function() {
    var idMap = [];
    function y(e) { 
        idMap.push(e); 
    }
    var idx = lunr(function() {
        this.field('title', { boost: 10 });
        this.field('content');
        this.field('description', { boost: 5 });
        this.field('tags', { boost: 50 });
        this.ref('id');
        this.tokenizer(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
    });
    function a(e) { 
        idx.add(e); 
    }

    a({
        id:0,
        title:"MkDocsServeAsyncSettings",
        content:"MkDocsServeAsyncSettings",
        description:'',
        tags:''
    });

    a({
        id:1,
        title:"MkDocsTheme",
        content:"MkDocsTheme",
        description:'',
        tags:''
    });

    a({
        id:2,
        title:"MkDocsTool",
        content:"MkDocsTool",
        description:'',
        tags:''
    });

    a({
        id:3,
        title:"MkDocsAddress",
        content:"MkDocsAddress",
        description:'',
        tags:''
    });

    a({
        id:4,
        title:"MkDocsServeRunner",
        content:"MkDocsServeRunner",
        description:'',
        tags:''
    });

    a({
        id:5,
        title:"MkDocsServeAsyncRunner",
        content:"MkDocsServeAsyncRunner",
        description:'',
        tags:''
    });

    a({
        id:6,
        title:"MkDocsArgumentAttribute",
        content:"MkDocsArgumentAttribute",
        description:'',
        tags:''
    });

    a({
        id:7,
        title:"MkDocsVersionRunner",
        content:"MkDocsVersionRunner",
        description:'',
        tags:''
    });

    a({
        id:8,
        title:"MkDocsServeSettings",
        content:"MkDocsServeSettings",
        description:'',
        tags:''
    });

    a({
        id:9,
        title:"MkDocsNewRunner",
        content:"MkDocsNewRunner",
        description:'',
        tags:''
    });

    a({
        id:10,
        title:"MkDocsGhDeploySettings",
        content:"MkDocsGhDeploySettings",
        description:'',
        tags:''
    });

    a({
        id:11,
        title:"MkDocsAliases",
        content:"MkDocsAliases",
        description:'',
        tags:''
    });

    a({
        id:12,
        title:"MkDocsNewSettings",
        content:"MkDocsNewSettings",
        description:'',
        tags:''
    });

    a({
        id:13,
        title:"MkDocsArgumentValueAttribute",
        content:"MkDocsArgumentValueAttribute",
        description:'',
        tags:''
    });

    a({
        id:14,
        title:"MkDocsSettings",
        content:"MkDocsSettings",
        description:'',
        tags:''
    });

    a({
        id:15,
        title:"MkDocsVersionSettings",
        content:"MkDocsVersionSettings",
        description:'',
        tags:''
    });

    a({
        id:16,
        title:"MkDocsBuildSettings",
        content:"MkDocsBuildSettings",
        description:'',
        tags:''
    });

    a({
        id:17,
        title:"MkDocsBuildRunner",
        content:"MkDocsBuildRunner",
        description:'',
        tags:''
    });

    a({
        id:18,
        title:"MkDocsGhDeployRunner",
        content:"MkDocsGhDeployRunner",
        description:'',
        tags:''
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs.Serve/MkDocsServeAsyncSettings',
        title:"MkDocsServeAsyncSettings",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs/MkDocsTheme',
        title:"MkDocsTheme",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs/MkDocsTool_1',
        title:"MkDocsTool<TSettings>",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs/MkDocsAddress',
        title:"MkDocsAddress",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs.Serve/MkDocsServeRunner',
        title:"MkDocsServeRunner",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs.Serve/MkDocsServeAsyncRunner',
        title:"MkDocsServeAsyncRunner",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs.Attributes/MkDocsArgumentAttribute',
        title:"MkDocsArgumentAttribute",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs.Version/MkDocsVersionRunner',
        title:"MkDocsVersionRunner",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs.Serve/MkDocsServeSettings',
        title:"MkDocsServeSettings",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs.New/MkDocsNewRunner',
        title:"MkDocsNewRunner",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs.GhDeploy/MkDocsGhDeploySettings',
        title:"MkDocsGhDeploySettings",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs/MkDocsAliases',
        title:"MkDocsAliases",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs.New/MkDocsNewSettings',
        title:"MkDocsNewSettings",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs.Attributes/MkDocsArgumentValueAttribute',
        title:"MkDocsArgumentValueAttribute",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs/MkDocsSettings',
        title:"MkDocsSettings",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs.Version/MkDocsVersionSettings',
        title:"MkDocsVersionSettings",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs.Build/MkDocsBuildSettings',
        title:"MkDocsBuildSettings",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs.Build/MkDocsBuildRunner',
        title:"MkDocsBuildRunner",
        description:""
    });

    y({
        url:'/Cake.MkDocs/api/Cake.MkDocs.GhDeploy/MkDocsGhDeployRunner',
        title:"MkDocsGhDeployRunner",
        description:""
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
