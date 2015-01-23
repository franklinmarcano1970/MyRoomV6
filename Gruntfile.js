module.exports = function (grunt) {

  grunt.initConfig({
    connect: {
      server: {
        options: {
          port: 9000,
          base: 'MyRoom.Web/'
        }
      }
    },
    watch: {
      project: {
        files: ['MyRoom.Web/**/*.js', 'MyRoom.Web/**/*.html', 'MyRoom.Web/**/*.json'],
        options: {
          livereload: true
        }
      }
    }
  });

  grunt.loadNpmTasks('grunt-contrib-connect');
  grunt.loadNpmTasks('grunt-contrib-watch');

  grunt.registerTask('default', ['connect', 'watch']);

};
