
var camelCaseTokenizer = function (builder) {

  var pipelineFunction = function (token) {
    var previous = '';
    // split camelCaseString to on each word and combined words
    // e.g. camelCaseTokenizer -> ['camel', 'case', 'camelcase', 'tokenizer', 'camelcasetokenizer']
    var tokenStrings = token.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
      var current = cur.toLowerCase();
      if (acc.length === 0) {
        previous = current;
        return acc.concat(current);
      }
      previous = previous.concat(current);
      return acc.concat([current, previous]);
    }, []);

    // return token for each string
    // will copy any metadata on input token
    return tokenStrings.map(function(tokenString) {
      return token.clone(function(str) {
        return tokenString;
      })
    });
  }

  lunr.Pipeline.registerFunction(pipelineFunction, 'camelCaseTokenizer')

  builder.pipeline.before(lunr.stemmer, pipelineFunction)
}
var searchModule = function() {
    var documents = [];
    var idMap = [];
    function a(a,b) { 
        documents.push(a);
        idMap.push(b); 
    }

    a(
        {
            id:0,
            title:"MkDocsNewRunner",
            content:"MkDocsNewRunner",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs.New/MkDocsNewRunner',
            title:"MkDocsNewRunner",
            description:""
        }
    );
    a(
        {
            id:1,
            title:"MkDocsBuildRunner",
            content:"MkDocsBuildRunner",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs.Build/MkDocsBuildRunner',
            title:"MkDocsBuildRunner",
            description:""
        }
    );
    a(
        {
            id:2,
            title:"MkDocsAliases",
            content:"MkDocsAliases",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs/MkDocsAliases',
            title:"MkDocsAliases",
            description:""
        }
    );
    a(
        {
            id:3,
            title:"MkDocsArgumentAttribute",
            content:"MkDocsArgumentAttribute",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs.Attributes/MkDocsArgumentAttribute',
            title:"MkDocsArgumentAttribute",
            description:""
        }
    );
    a(
        {
            id:4,
            title:"MkDocsSettings",
            content:"MkDocsSettings",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs/MkDocsSettings',
            title:"MkDocsSettings",
            description:""
        }
    );
    a(
        {
            id:5,
            title:"MkDocsVersionSettings",
            content:"MkDocsVersionSettings",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs.Version/MkDocsVersionSettings',
            title:"MkDocsVersionSettings",
            description:""
        }
    );
    a(
        {
            id:6,
            title:"MkDocsServeRunner",
            content:"MkDocsServeRunner",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs.Serve/MkDocsServeRunner',
            title:"MkDocsServeRunner",
            description:""
        }
    );
    a(
        {
            id:7,
            title:"MkDocsGhDeploySettings",
            content:"MkDocsGhDeploySettings",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs.GhDeploy/MkDocsGhDeploySettings',
            title:"MkDocsGhDeploySettings",
            description:""
        }
    );
    a(
        {
            id:8,
            title:"MkDocsBuildSettings",
            content:"MkDocsBuildSettings",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs.Build/MkDocsBuildSettings',
            title:"MkDocsBuildSettings",
            description:""
        }
    );
    a(
        {
            id:9,
            title:"MkDocsServeAsyncRunner",
            content:"MkDocsServeAsyncRunner",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs.Serve/MkDocsServeAsyncRunner',
            title:"MkDocsServeAsyncRunner",
            description:""
        }
    );
    a(
        {
            id:10,
            title:"MkDocsServeAsyncSettings",
            content:"MkDocsServeAsyncSettings",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs.Serve/MkDocsServeAsyncSettings',
            title:"MkDocsServeAsyncSettings",
            description:""
        }
    );
    a(
        {
            id:11,
            title:"MkDocsTool",
            content:"MkDocsTool",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs/MkDocsTool_1',
            title:"MkDocsTool<TSettings>",
            description:""
        }
    );
    a(
        {
            id:12,
            title:"MkDocsServeSettings",
            content:"MkDocsServeSettings",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs.Serve/MkDocsServeSettings',
            title:"MkDocsServeSettings",
            description:""
        }
    );
    a(
        {
            id:13,
            title:"MkDocsGhDeployRunner",
            content:"MkDocsGhDeployRunner",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs.GhDeploy/MkDocsGhDeployRunner',
            title:"MkDocsGhDeployRunner",
            description:""
        }
    );
    a(
        {
            id:14,
            title:"MkDocsTheme",
            content:"MkDocsTheme",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs/MkDocsTheme',
            title:"MkDocsTheme",
            description:""
        }
    );
    a(
        {
            id:15,
            title:"MkDocsAddress",
            content:"MkDocsAddress",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs/MkDocsAddress',
            title:"MkDocsAddress",
            description:""
        }
    );
    a(
        {
            id:16,
            title:"MkDocsVersionRunner",
            content:"MkDocsVersionRunner",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs.Version/MkDocsVersionRunner',
            title:"MkDocsVersionRunner",
            description:""
        }
    );
    a(
        {
            id:17,
            title:"MkDocsNewSettings",
            content:"MkDocsNewSettings",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs.New/MkDocsNewSettings',
            title:"MkDocsNewSettings",
            description:""
        }
    );
    a(
        {
            id:18,
            title:"MkDocsArgumentValueAttribute",
            content:"MkDocsArgumentValueAttribute",
            description:'',
            tags:''
        },
        {
            url:'/Cake.MkDocs/api/Cake.MkDocs.Attributes/MkDocsArgumentValueAttribute',
            title:"MkDocsArgumentValueAttribute",
            description:""
        }
    );
    var idx = lunr(function() {
        this.field('title');
        this.field('content');
        this.field('description');
        this.field('tags');
        this.ref('id');
        this.use(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
        documents.forEach(function (doc) { this.add(doc) }, this)
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
