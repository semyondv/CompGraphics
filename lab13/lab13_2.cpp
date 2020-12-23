#include "GL/glew.h"
#include "GL/freeglut.h"
#include <iostream>
#include "stb_image.h"

// Вершинный шейдер.
const char* vertexShaderSource = R"(
#version 330 core
uniform vec3 angle;

layout(location = 0) in vec3 aPos;
layout(location = 1) in vec3 aColor;
layout(location = 2) in vec2 aTexCoord;

out vec3 ourColor;
out vec2 TexCoord;

mat3 rotX(float ang) {
    return mat3(
        1.0, 0.0, 0.0,
        0.0, cos(ang), -sin(ang),
        0.0, sin(ang), cos(ang));
}

mat3 rotZ(float ang) {
    return mat3(
        cos(ang), -sin(ang), 0.0,
        sin(ang), cos(ang), 0.0,
        0.0, 0.0, 1.0);
}

void main() {
    mat3 matr = rotX(angle.x) * rotZ(angle.z);

    vec3 pos = matr * aPos;

    gl_Position = vec4(pos, 1.0);
    ourColor = aColor;
    TexCoord = aTexCoord;
})";


// Фрагментный шейдер.
const char* fragmentShaderSource = R"(
#version 330 core

out vec4 FragColor;

in vec3 ourColor;
in vec2 TexCoord;

uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
    FragColor = mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.4);
})";

GLuint program;
GLuint texture1;
GLuint texture2;

GLuint VBO;
GLuint VAO;
GLuint EBO;


double rotate_x = 0;
double rotate_y = 0;
double rotate_z = 0;

void setVBO() {

    float vertices[] = {
        // coordinates          colors              tex coords
        -0.3f, -0.3f, 0.3f,     1.0f, 0.0f, 0.0f,   1.0f, 1.0f,
        -0.3f,  0.3f, 0.3f,     0.0f, 1.0f, 0.0f,   1.0f, 0.0f,
         0.3f,  0.3f, 0.3f,     0.0f, 0.0f, 1.0f,   0.0f, 0.0f,
         0.3f, -0.3f, 0.3f,     1.0f, 1.0f, 1.0f,   0.0f, 1.0f,

        -0.3f, -0.3f, -0.3f,    1.0f, 0.0f, 0.0f,   1.0f, 1.0f,
        -0.3f,  0.3f, -0.3f,    0.0f, 1.0f, 0.0f,   1.0f, 0.0f,
         0.3f,  0.3f, -0.3f,    0.0f, 0.0f, 1.0f,   0.0f, 0.0f,
         0.3f, -0.3f, -0.3f,    1.0f, 1.0f, 1.0f,   0.0f, 1.0f,

        -0.3f, -0.3f, -0.3f,    1.0f, 0.0f, 0.0f,   0.0f, 1.0f,
        -0.3f,  0.3f, -0.3f,    0.0f, 1.0f, 0.0f,   0.0f, 0.0f,
         0.3f,  0.3f, -0.3f,    0.0f, 0.0f, 1.0f,   1.0f, 0.0f,
         0.3f, -0.3f, -0.3f,    1.0f, 1.0f, 1.0f,   1.0f, 1.0f,

        -0.3f,  0.3f, -0.3f,    0.0f, 1.0f, 0.0f,   1.0f, 1.0f,
         0.3f,  0.3f, -0.3f,    0.0f, 0.0f, 1.0f,   1.0f, 0.0f,
         //-0.3f, -0.3f, -0.3f,    1.0f, 0.0f, 0.0f,   1.0f, 1.0f,
         //-0.3f,  0.3f, -0.3f,    0.0f, 1.0f, 0.0f,   1.0f, 0.0f,
    };


    glGenBuffers(1, &VBO);
    glBindBuffer(GL_ARRAY_BUFFER, VBO);
    glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

    unsigned int indices[] = {
                 0, 3, 7, 4,
                 0, 1, 2, 3,
                 4, 7, 6, 5,
                 9, 1, 0, 8,
                 1, 12, 13, 2,
                 2, 10, 11, 3,
    };
    glGenBuffers(1, &EBO);
    glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
    glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);

    glGenVertexArrays(1, &VAO);

    glBindVertexArray(VAO);
    glBindBuffer(GL_ARRAY_BUFFER, VBO);
    glEnableVertexAttribArray(0);
    glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
    glEnableVertexAttribArray(1);
    glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(3 * sizeof(float)));
    glEnableVertexAttribArray(2);
    glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(6 * sizeof(float)));
    glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
    glBindVertexArray(0);
}


void render() {
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glClearDepth(1.0f);
    glEnable(GL_DEPTH_TEST);

    double pi = 3.1415;
    glUniform3f(0,
        (GLfloat)(rotate_x * pi / 180),
        (GLfloat)(rotate_y * pi / 180),
        (GLfloat)(rotate_z * pi / 180));

    glActiveTexture(GL_TEXTURE0);
    glBindTexture(GL_TEXTURE_2D, texture1);
    glActiveTexture(GL_TEXTURE1);
    glBindTexture(GL_TEXTURE_2D, texture2);
    glUniform1i(glGetUniformLocation(program, "texture1"), 0);
    glUniform1i(glGetUniformLocation(program, "texture2"), 1);

    glBindVertexArray(VAO);

    glDrawElements(GL_QUADS, 24, GL_UNSIGNED_INT, 0);
    glBindVertexArray(0);

    glFlush();

    glutSwapBuffers();
}

void update() {
    render();
}



void keySpecialFunc(int key, int x, int y) {

    switch (key) {
    case GLUT_KEY_UP:
        rotate_x -= 1.5;
        break;
    case GLUT_KEY_DOWN:
        rotate_x += 1.5;
        break;
    case GLUT_KEY_LEFT:
        rotate_z += 1.5;
        break;
    case GLUT_KEY_RIGHT:
        rotate_z -= 1.5;
        break;
    }
    glutPostRedisplay();
}

void freeShaders() {
    glUseProgram(0);
    glDeleteProgram(program);
}
void resizeWindow(int width, int height) {
    glViewport(0, 0, width, height);
}

void initialize() {

    GLuint vertexShader = glCreateShader(GL_VERTEX_SHADER);
    glShaderSource(vertexShader, 1, &vertexShaderSource, NULL);
    glCompileShader(vertexShader);

    GLuint fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
    glShaderSource(fragmentShader, 1, &fragmentShaderSource, NULL);
    glCompileShader(fragmentShader);

    program = glCreateProgram();
    glAttachShader(program, vertexShader);
    glAttachShader(program, fragmentShader);
    glLinkProgram(program);


    int width, height, chanels;
    stbi_set_flip_vertically_on_load(true);
    //unsigned char* data = stbi_load("box.jpg", &width, &height, &chanels, 0);
    //unsigned char* data = stbi_load("box2.jpg", &width, &height, &chanels, 0);
    unsigned char* data = stbi_load("chess.jpg", &width, &height, &chanels, 0);

    glActiveTexture(GL_TEXTURE0);
    glGenTextures(1, &texture1);
    glBindTexture(GL_TEXTURE_2D, texture1);
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, data);
    glGenerateMipmap(GL_TEXTURE_2D);

    data = stbi_load("noise.jpg", &width, &height, &chanels, 0);
    glGenTextures(1, &texture2);
    glBindTexture(GL_TEXTURE_2D, texture2);
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, data);
    glGenerateMipmap(GL_TEXTURE_2D);

    setVBO();

    glUseProgram(program);
}

int main(int argc, char** argv) {
    glutInit(&argc, argv);
    glutInitDisplayMode(GLUT_DEPTH | GLUT_RGBA | GLUT_ALPHA | GLUT_DOUBLE);
    glutInitWindowSize(600, 600);
    glutCreateWindow("OpenGL3");
    glClearColor(0, 0, 0, 0);
    //! Обязательно перед инициализацией шейдеров
    GLenum glew_status = glewInit();
    if (GLEW_OK != glew_status) {
        //! GLEW не проинициализировалась
        std::cout << "Error: " << glewGetErrorString(glew_status) << "\n";
        return 1;
    }
    //! Проверяем доступность OpenGL 2.0
    if (!GLEW_VERSION_2_0) {
        //! OpenGl 2.0 оказалась не доступна
        std::cout << "No support for OpenGL 2.0 found\n";
        return 1;
    }
    //! Инициализация шейдеров
    initialize();
    glutReshapeFunc(resizeWindow);
    glutDisplayFunc(render);
    glutIdleFunc(update);
    glutSpecialFunc(keySpecialFunc);
    glutMainLoop();
    //! Освобождение ресурсов
    freeShaders();
}