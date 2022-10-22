/// <binding BeforeBuild='build' />
/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    grunt.initConfig({
        less: {
            build: {
                options: {
                    paths: ["."],
                    plugins: [
                        new (require('less-plugin-autoprefix'))({ browsers: ["last 2 versions"] })
                    ],
                    compress: false
                },
                files: {
                    "site/siteX.css": "site/less/import.less"
                }
            }
        },

        cssmin: {
            build: {
                options: {
                    shorthandCompacting: false,
                    roundingPrecision: -1,
                    report: 'min',
                    level: {
                        1: {
                            specialComments: 0
                        }
                    }
                },
                files: {
                    'wwwroot/css/site.min.css': ['site/siteX.css']
                }
            }
        },


        cachebreaker: {
            build: {
                options: {
                    match: [
                        {
                            'site.min.js': 'wwwroot/js/site.min.js',
                            'site.min.css': 'wwwroot/css/site.min.css'
                        }
                    ],
                    replacement: 'md5'
                },


                files: {
                    src: ['Pages/Shared/_Layout.cshtml']
                }
            }
        },

        clean: {
            build: ['site/siteX.css']

        },
               
        uglify: {
            build: {
                options: {
                    //mangle: false,
                    //compress: false,
                    //beautify: true
                    //,
 
                    mangle: true,
                    compress: true,
                    beautify: false,
                    output: {
                        comments: false
                    }
                },

                files: {
                    'wwwroot/js/site.min.js': ['site/js/site.js']
                }
            }
        }

    });

    grunt.registerTask("default", [""]);
    grunt.registerTask("buildless", ["less:build"]);
    grunt.registerTask("builduglify", ["uglify:build"]);
    grunt.registerTask("buildcachebreaker", ["cachebreaker:build"]);
    grunt.registerTask("buildcssmin", ["cssmin:build"]);
    grunt.registerTask("buildclean", ["clean:build"]);
    grunt.registerTask("build", ["buildless", "builduglify", "buildcssmin", "buildcachebreaker", "buildclean"]);

    grunt.loadNpmTasks("grunt-contrib-less");
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-cache-breaker'); 
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-copy');
 
};



